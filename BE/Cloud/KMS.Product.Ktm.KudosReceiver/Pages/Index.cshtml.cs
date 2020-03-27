using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMS.Product.Ktm.KudosReceiver.SlackClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SlackAPI.WebSocketMessages;

namespace KMS.Product.Ktm.KudosReceiver.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var slackClient = SlackClient.SlackFixture.Instance().BotClient;
            slackClient.SendPresence(SlackAPI.Presence.auto);
            slackClient.PostMessage(response =>
            {
            }, "#test-kmskudos", "Hey, I'm on.");
            JoinDirectMessageChannel();
        }
        public void JoinDirectMessageChannel()
        {
            // given
            var client = SlackClient.SlackFixture.Instance().BotClient;

            string userName = SlackClient.SlackFixture.Instance().Config.DirectMessageUser;
            string user = client.Users.First(x => x.name.Equals(userName, StringComparison.OrdinalIgnoreCase)).id;

            // when
            using (var sync = new InSync("SlackClient.JoinDirectMessageChannel"))
            {
                client.JoinDirectMessageChannel(response =>
                {
                    Console.WriteLine($"Joined {response.channel.id}.");
                    client.PostMessage(null, response.channel.id, "Hi how are you");
                    sync.Proceed(); ;
                }, user);
            }
        }


    }
}
