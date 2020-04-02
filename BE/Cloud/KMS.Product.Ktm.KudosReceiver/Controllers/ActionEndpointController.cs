using KMS.Product.Ktm.KudosReceiver.Models.Events;
using KMS.Product.Ktm.KudosReceiver.SlackClient;
using Microsoft.AspNetCore.Mvc;
using SlackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KMS.Product.Ktm.KudosReceiver.Controllers
{
    [Route("slack/[controller]")]
    [ApiController]
    public class ActionEndpointController : ControllerBase
    {
        // GET: slack/InteractiveEnpoint
        [HttpGet]
        public SlackEvent Get()
        {
            return new SlackEvent { Challenge = "123" };
        }

        // POST: slack/InteractiveEnpoint
        [HttpPost]
        public string Body([FromBody] SlackEvent data)
        {
            var result = data?.Challenge;
            var slackFixture = SlackClient.SlackFixture.Instance();
            var sender = slackFixture.Users?[data?.Event.User];
            if (sender?.is_bot ?? true) return result;
            SendConfirmationResponse(sender, data?.Event.Text);
            SendInformMessages(sender, data?.Event.Text);
            return result;
        }
        private void SendConfirmationResponse(User sender, string originalMessage)
        {
            var slackBot = SlackClient.SlackFixture.Instance().BotClient;
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
        private void SendInformMessages(User sender, string originalMessage)
        {
            var slackBot = SlackClient.SlackFixture.Instance().BotClient;
            var messages = GenerateInformMessages(sender, originalMessage, SlackClient.SlackFixture.Instance());
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
        private string GenerateConfirmMessage(User sender, string message)
        {
            var receivers = GetNameOfReceivers(message).Where(r => r != sender.id).ToList();
            return !receivers.Any() ? string.Empty : $":sun_with_face: {string.Join(" ", receivers)} recevied *{GetKudosTypes(message).Count()} kudos* from you, {sender.real_name}";
        }
        private IEnumerable<Tuple<User, string>> GenerateInformMessages(User sender, string message, SlackFixture slackFixture)
        {
            return GetReceivers(message, slackFixture)
                   .Where(receiver => receiver.id != sender.id)
                   .Select(receiver => Tuple.Create(
            receiver, $":sun_with_face: Congrats, You received *{GetKudosTypes(message).Count()} kudos* from <@{sender?.id}>."));
        }
        private IEnumerable<string> GetKudosTypes(string message)
        {
            return Regex.Matches(message, ":[\\w]+:")?.Select(o => o.Value);
        }
        private IEnumerable<string> GetNameOfReceivers(string message)
        {
            return Regex.Matches(message, "<@[\\w]+>")?.Select(o => o.Value);
        }
        private IEnumerable<User> GetReceivers(string message, SlackFixture slackFixture)
        {
            return Regex.Matches(message, "<@[\\w]+>")?.Select(o => slackFixture.Users[o.Value.Substring(2, o.Value.Length - 3)]).Distinct();
        }
    }
}
