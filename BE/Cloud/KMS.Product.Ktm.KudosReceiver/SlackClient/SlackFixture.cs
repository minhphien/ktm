using KMS.Product.Ktm.KudosReceiver.SlackClient.Models;
using Microsoft.Extensions.Configuration;
using Polly;
using SlackAPI;
using SlackAPI.WebSocketMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KMS.Product.Ktm.KudosReceiver.SlackClient
{
    public class SlackFixture : IDisposable
    {
        private const int MaxConnectionAttempts = 5;

        private readonly Lazy<SlackSocketClient> userClient;
        private readonly Lazy<SlackSocketClient> botClient;
        private readonly Lazy<SlackTaskClient> userClientAsync;
        private readonly Lazy<SlackTaskClient> botClientAsync;

        private readonly Policy connectRetryPolicy;

        public string HostEnvironment { get; set; }
        private SlackFixture(IConfiguration configuration)
        {
            this.Config = this.GetConfig(configuration);

            this.connectRetryPolicy = Policy
                .Handle<InvalidOperationException>(exception => exception.Message.Contains("ratelimited"))
                .WaitAndRetry(MaxConnectionAttempts, retryAttempt => TimeSpan.FromSeconds(ComputeExponentialBackoff(retryAttempt)),
                    (exception, timeSpan, retryCount, context) => Console.WriteLine($"Connection failed ({exception.Message}). Retrying after {timeSpan.TotalSeconds}s ({retryCount}/5)"));

            this.userClient = new Lazy<SlackSocketClient>(() => connectRetryPolicy.Execute(() => this.CreateClient(this.Config.UserAuthToken)));
            this.botClient = new Lazy<SlackSocketClient>(() => connectRetryPolicy.Execute(() => this.CreateClient(this.Config.BotAuthToken)));
            this.userClientAsync = new Lazy<SlackTaskClient>(() => new SlackTaskClient(this.Config.UserAuthToken));
            this.botClientAsync = new Lazy<SlackTaskClient>(() => new SlackTaskClient(this.Config.BotAuthToken));
            PopulateData();
        }

        private void PopulateData()
        {
            using (var syncClient = new InSync($"{nameof(SlackFixture.PopulateData)} - Connected callback"))
            {
                this.UserClient.GetUserList(list =>
                {
                    Console.WriteLine($"Obtained user list. Count: {list?.members?.Length}");
                    this.Users = list?.members?.ToDictionary(x => x.id, x => x);
                });
                syncClient.Proceed();
            }
        }

        static SlackFixture instance = null;
        public Dictionary<string, User> Users { get; set; }
        
        public static SlackFixture Instance()
        {
            return instance;
        }
        public static void Initialize(IConfiguration configuration)
        {
            instance ??= new SlackFixture(configuration);
        }
        public SlackConfig Config { get; }

        public TimeSpan ConnectionTimeout => TimeSpan.FromSeconds(Enumerable.Range(1, MaxConnectionAttempts).Sum(ComputeExponentialBackoff)) + TimeSpan.FromSeconds(10); // Maximum exponential backoff + 10 seconds for connections attemps

        public SlackSocketClient UserClient
        {
            get
            {
                return userClient.Value;
            }
        }

        public SlackSocketClient BotClient
        {
            get
            {
                return botClient.Value;
            }
        }

        public SlackSocketClient CreateUserClient(IWebProxy proxySettings = null, bool maintainPresenceChangesStatus = false, Action<SlackSocketClient, PresenceChange> presenceChanged = null)
        {
            return this.connectRetryPolicy.Execute(() => this.CreateClient(this.Config.UserAuthToken, proxySettings, maintainPresenceChangesStatus, presenceChanged));
        }

        public SlackSocketClient CreateBotClient(IWebProxy proxySettings = null)
        {
            return this.connectRetryPolicy.Execute(() => this.CreateClient(this.Config.BotAuthToken, proxySettings));
        }

        public SlackTaskClient UserClientAsync => userClientAsync.Value;

        public SlackTaskClient BotClientAsync => botClientAsync.Value;

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

        private SlackConfig GetConfig(IConfiguration configuration)
        {
            var slackConfig = new SlackConfig();
            configuration.GetSection("slack")?.Bind(slackConfig);
            return slackConfig;
        }

        private SlackSocketClient CreateClient(string authToken, IWebProxy proxySettings = null, bool maintainPresenceChanges = false, Action<SlackSocketClient, PresenceChange> presenceChanged = null)
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

        private int ComputeExponentialBackoff(int retryAttempt)
        {
            // Retries after 4, 8, 16, 32, 64... seconds
            return 2 * (int)Math.Pow(2, retryAttempt);
        }
    }
}
