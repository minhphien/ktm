using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Repository;
using Microsoft.Extensions.Configuration;
using SlackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KMS.Product.Ktm.Services.SlackService
{
    public partial class SlackService : ISlackService
    {
        private readonly IKudosDetailRepository kudosDetailRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly SlackFixture slackFixture;

        #region Private Methods
        private string GenerateConfirmMessage(User sender, IEnumerable<string> receivers, int kudosCount)
        {
            return !receivers.Any() ? string.Empty : $":sun_with_face: {string.Join(" ", receivers)} recevied *{kudosCount} kudos* from you, {sender.real_name}";
        }
        private IEnumerable<Tuple<string, string>> GenerateInformMessages(User sender, IEnumerable<string> receiverSlackIds, int kudosCount)
        {
            return receiverSlackIds?.Select(Id => Tuple.Create(
            Id, $":sun_with_face: Congrats, You received *{kudosCount.ToString()} kudos* from <@{sender?.id}>."));
        }

        private IEnumerable<string> GetEmojies(string message)
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
        #endregion
        public SlackService(IConfiguration configuration, IKudosDetailRepository kudosDetailRepository, IEmployeeRepository employeeRepository)
        {
            this.kudosDetailRepository = kudosDetailRepository;
            this.employeeRepository = employeeRepository;
            SlackFixture.Intialize(configuration);
            slackFixture = SlackFixture.Instance;
        }

        #region Public Methods

        public Dictionary<string, User> Users
        {
            get
            {
                return slackFixture.Users;
            }
        }

        

        /// <summary>Sends the confirmation response.</summary>
        /// <param name="sender">The Slack sender.</param>
        /// <param name="originalMessage">The original message.</param>
        private void SendConfirmationResponse(User sender, IEnumerable<string> receivers, int kudosCount)
        {
            var slackBot = slackFixture.BotClient;
            var message = GenerateConfirmMessage(sender, receivers, kudosCount);
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
        private void SendInformMessages(User sender, IEnumerable<string> receiverSlackIds, int kudosCount)
        {
            var slackBot = slackFixture.BotClient;
            var messages = GenerateInformMessages(sender, receiverSlackIds, kudosCount);
            foreach ((var receiver, var message) in messages)
            {
                using (var syncDirectJoint = new InSync($"JoinDirectMessageChannel - {receiver}"))
                {
                    slackBot.JoinDirectMessageChannel(response =>
                    {
                        using (var syncPosting = new InSync($"DirectMessagePosting - {receiver}"))
                        {
                            slackBot.PostMessage(response =>
                            {
                                syncPosting.Proceed();
                            }, response.channel?.id, message);
                        }
                        syncDirectJoint.Proceed();
                    }, receiver);
                }
            }
        }

        /// <summary>Proceeds the recevied message.</summary>
        /// <param name="slackUserId">The slack user ID.</param>
        /// <param name="message">The message.</param>
        public void ProceedReceviedMessage(string slackUserId, string message)
        {
            var sender = slackFixture.Users?[slackUserId];
            var receivers = GetNameOfReceivers(message).Where(r => r != sender.id);
            var kudosDetails = kudosDetailRepository.GetKudosDetailBySlackEmoji(GetEmojies(message));
            SendConfirmationResponse(sender, receivers, kudosDetails.Count());
            SendInformMessages(sender, receivers, kudosDetails.Count());
        }
        #endregion
    }
}
