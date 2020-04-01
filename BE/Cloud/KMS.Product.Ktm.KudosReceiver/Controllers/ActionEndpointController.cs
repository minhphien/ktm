using KMS.Product.Ktm.KudosReceiver.Models.Events;
using KMS.Product.Ktm.KudosReceiver.SlackClient;
using Microsoft.AspNetCore.Mvc;
using SlackAPI;
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
            
            LogToChannel("#test-kmskudos", data);
            return data?.Challenge;
        }
        private void LogToChannel(string channel, SlackEvent data)
        {

            var slackFixture = SlackClient.SlackFixture.Instance();
            var sender = (slackFixture.Users?.Where(o => o.Value.id == data.Event.User).FirstOrDefault())?.Value;
            if (!(sender?.is_bot ?? true))
            {
                var slackBot = SlackClient.SlackFixture.Instance().BotClient;
                var message = GenerateReplyContent(sender, data?.Event.Text);
                
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
        }
        private string GenerateReplyContent(User sender, string message)
        {
            var receivers = GetReceivers(message).ToList();
            return !receivers.Any() ? string.Empty : $":sun_with_face: {string.Join(" ", receivers)} recevied *{GetKudosTypes(message).Count()} kudos* from you, {sender.name}";
        }
        private IEnumerable<string> GetKudosTypes(string message)
        {
            return Regex.Matches(message, ":[\\w]+:")?.Select(o => o.Value);
        }
        private IEnumerable<string> GetReceivers(string message)
        {
            return Regex.Matches(message, "<@[\\w]+>")?.Select(o => o.Value);
        }
    }
}
