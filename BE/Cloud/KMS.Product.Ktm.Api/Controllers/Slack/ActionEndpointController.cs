using KMS.Product.Ktm.Dto;
using KMS.Product.Ktm.Services.SlackService;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.KudosReceiver.Controllers
{
    [Route("slack/[controller]")]
    [ApiController]
    public class ActionEndpointController : ControllerBase
    {
        private readonly ISlackService slackService;

        public ActionEndpointController(ISlackService slackService)
        {
            this.slackService = slackService;
        }

        /// <summary>Get Method for testing.</summary>
        /// <returns>Test data</returns>
        [HttpGet]
        public SlackEventDto Get()
        {
            return new SlackEventDto { Challenge = "OK" };
        }

        /// <summary>Post method the specified data.</summary>
        /// <param name="data">The Slack request payload.</param>
        /// <returns>data.Challenge</returns>
        [HttpPost]
        public string Post([FromBody] SlackEventDto data)
        {
            var result = data?.Challenge;
            var sender = slackService.Users?[data.Event?.User];
            if (sender?.is_bot ?? true) return result;
            slackService.ProceedReceviedMessage(sender.id, data.Event.Text);
            return result;
        }
    }
}
