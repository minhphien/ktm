using Microsoft.Extensions.Configuration;
using Polly;
using SlackAPI;
using SlackAPI.WebSocketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace KMS.Product.Ktm.Services.SlackService
{
    public class SlackService : ISlackService, IDisposable
    {
        private const int MaxConnectionAttempts = 5;
        private readonly Lazy<SlackSocketClient> botClient;
        private readonly Lazy<SlackTaskClient> botClientAsync;
        private readonly Policy connectRetryPolicy;
        private readonly Lazy<SlackSocketClient> userClient;
        private readonly Lazy<SlackTaskClient> userClientAsync;
        public SlackService(IConfiguration configuration)
        {
            this.Config = this.GetHostConfig(configuration);

            this.connectRetryPolicy = Policy
                .Handle<InvalidOperationException>(exception => exception.Message.Contains("ratelimited"))
                .WaitAndRetry(MaxConnectionAttempts, retryAttempt => TimeSpan.FromSeconds(ComputeExponentialBackoff(retryAttempt)),
                    (exception, timeSpan, retryCount, context) => Console.WriteLine($"Connection failed ({exception.Message}). Retrying after {timeSpan.TotalSeconds}s ({retryCount}/5)"));

            this.userClient = new Lazy<SlackSocketClient>(() => connectRetryPolicy.Execute(() => this.CreateClient(this.Config.UserAuthToken)));
            this.botClient = new Lazy<SlackSocketClient>(() => connectRetryPolicy.Execute(() => this.CreateClient(this.Config.BotAuthToken)));
            this.userClientAsync = new Lazy<SlackTaskClient>(() => new SlackTaskClient(this.Config.UserAuthToken));
            this.botClientAsync = new Lazy<SlackTaskClient>(() => new SlackTaskClient(this.Config.BotAuthToken));
            FetchData();
        }
        public SlackConfig Config { get; }

        public TimeSpan ConnectionTimeout => TimeSpan.FromSeconds(Enumerable.Range(1, MaxConnectionAttempts).Sum(ComputeExponentialBackoff)) + TimeSpan.FromSeconds(10);

        public Dictionary<string, User> Users { get; set; }

        protected SlackSocketClient BotClient
        {
            get
            {
                return botClient.Value;
            }
        }
        protected SlackTaskClient BotClientAsync => botClientAsync.Value;
        protected SlackSocketClient UserClient
        {
            get
            {
                return userClient.Value;
            }
        }
        protected SlackTaskClient UserClientAsync => userClientAsync.Value;
        public void Dispose()
        {
            if (this.userClient.IsValueCreated)
            {
                this.UserClient.CloseSocket();
            }

            if (this.botClient.IsValueCreated)
            {
                this.BotClient.CloseSocket();
            }
        }

        /// <summary>Sends the confirmation response.</summary>
        /// <param name="sender">The Slack sender.</param>
        /// <param name="originalMessage">The original message.</param>
        public void SendConfirmationResponse(User sender, string originalMessage)
        {
            var slackBot = this.BotClient;
            var message = GenerateConfirmMessage(sender, originalMessage);
            using (var syncDirectJoint = new InSync("JoinDirectMessageChannel"))
            {
                slackBot.JoinDirectMessageChannel(response =>
                {
                    using (var syncPosting = new InSync("DirectMessagePosting"))
                    {
                        slackBot.PostMessage(response =>
                        {
                            syncPosting.Proceed();
                        }, response.channel?.id, message);
                    }
                    syncDirectJoint.Proceed();
                }, sender.id);
            }
        }

        /// <summary>Sends the inform messages.</summary>
        /// <param name="sender">The Slack sender.</param>
        /// <param name="originalMessage">The original message.</param>
        public void SendInformMessages(User sender, string originalMessage)
        {
            var slackBot = this.BotClient;
            var messages = GenerateInformMessages(sender, originalMessage);
            foreach ((var receiver, var message) in messages)
            {
                using (var syncDirectJoint = new InSync($"JoinDirectMessageChannel - {receiver.id}"))
                {
                    slackBot.JoinDirectMessageChannel(response =>
                    {
                        using (var syncPosting = new InSync($"DirectMessagePosting - {receiver.id}"))
                        {
                            slackBot.PostMessage(response =>
                            {
                                syncPosting.Proceed();
                            }, response.channel?.id, message);
                        }
                        syncDirectJoint.Proceed();
                    }, receiver.id);
                }
            }
        }

        protected SlackSocketClient CreateBotClient(IWebProxy proxySettings = null)
        {
            return this.connectRetryPolicy.Execute(() => this.CreateClient(this.Config.BotAuthToken, proxySettings));
        }
        protected SlackSocketClient CreateClient(string authToken, IWebProxy proxySettings = null, bool maintainPresenceChanges = false, Action<SlackSocketClient, PresenceChange> presenceChanged = null)
        {
            SlackSocketClient client;

            LoginResponse loginResponse = null;
            using (var syncClient = new InSync($"{nameof(SlackAPI.SlackClient.Connect)} - Connected callback"))
            using (var syncClientSocket = new InSync($"{nameof(SlackAPI.SlackClient.Connect)} - SocketConnected callback"))
            using (var syncClientSocketHello = new InSync($"{nameof(SlackAPI.SlackClient.Connect)} - SocketConnected hello callback"))
            {
                client = new SlackSocketClient(authToken, proxySettings, maintainPresenceChanges);

                void OnPresenceChanged(PresenceChange x)
                {
                    presenceChanged?.Invoke(client, x);
                }

                client.OnPresenceChanged += OnPresenceChanged;
                client.OnHello += () => syncClientSocketHello.Proceed();
                client.Connect(x =>
                {
                    loginResponse = x;

                    Console.WriteLine($"Connected {x.ok}");
                    syncClient.Proceed();
                    if (!x.ok)
                    {
                        // If connect fails, socket connect callback is not called
                        syncClientSocket.Proceed();
                        syncClientSocketHello.Proceed();
                    }
                }, () =>
                {
                    Console.WriteLine("Socket Connected");
                    syncClientSocket.Proceed();
                });
            }


            return client;
        }

        protected SlackSocketClient CreateUserClient(IWebProxy proxySettings = null, bool maintainPresenceChangesStatus = false, Action<SlackSocketClient, PresenceChange> presenceChanged = null)
        {
            return this.connectRetryPolicy.Execute(() => this.CreateClient(this.Config.UserAuthToken, proxySettings, maintainPresenceChangesStatus, presenceChanged));
        }
        private int ComputeExponentialBackoff(int retryAttempt)
        {
            // Retries after 4, 8, 16, 32, 64... seconds
            return 2 * (int)Math.Pow(2, retryAttempt);
        }
        private void FetchData()
        {
            using (var syncClient = new InSync($"{nameof(SlackService.FetchData)} - Connected callback"))
            {
                this.UserClient.GetUserList(list =>
                {
                    Console.WriteLine($"Obtained user list. Count: {list?.members?.Length}");
                    this.Users = list?.members?.ToDictionary(x => x.id, x => x);
                });
                syncClient.Proceed();
            }
        }
        private string GenerateConfirmMessage(User sender, string message)
        {
            var receivers = GetNameOfReceivers(message).Where(r => r != sender.id).ToList();
            return !receivers.Any() ? string.Empty : $":sun_with_face: {string.Join(" ", receivers)} recevied *{GetKudosTypes(message).Count()} kudos* from you, {sender.real_name}";
        }
        private IEnumerable<Tuple<User, string>> GenerateInformMessages(User sender, string message)
        {
            return GetReceivers(message)
                   .Where(receiver => receiver.id != sender.id)
                   .Select(receiver => Tuple.Create(
            receiver, $":sun_with_face: Congrats, You received *{GetKudosTypes(message).Count()} kudos* from <@{sender?.id}>."));
        }
        private SlackConfig GetHostConfig(IConfiguration configuration)
        {
            var slackConfig = new SlackConfig();
            configuration.GetSection("slack")?.Bind(slackConfig);
            return slackConfig;
        }
        private IEnumerable<string> GetKudosTypes(string message)
        {
            return Regex.Matches(message, ":[\\w]+:")?.Select(o => o.Value);
        }
        private IEnumerable<string> GetNameOfReceivers(string message)
        {
            return Regex.Matches(message, "<@[\\w]+>")?.Select(o => o.Value);
        }
        private IEnumerable<User> GetReceivers(string message)
        {
            return Regex.Matches(message, "<@[\\w]+>")?.Select(o => this.Users[o.Value.Substring(2, o.Value.Length - 3)]).Distinct();
        }
    }
}
