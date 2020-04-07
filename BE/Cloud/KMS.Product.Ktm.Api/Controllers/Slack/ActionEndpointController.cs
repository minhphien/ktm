using KMS.Product.Ktm.Dto;
using KMS.Product.Ktm.Services.SlackService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KMS.Product.Ktm.KudosReceiver.Controllers
{
    [Route("api/slack/[controller]")]
    [ApiController]
    public class ActionEndpointController : ControllerBase
    {
        private readonly ISlackService _slackService;

        /// <summary>
        /// Init ActionEndpointController
        /// </summary>
        /// <param name="slackService"></param>
        public ActionEndpointController(ISlackService slackService)
        {
            _slackService = slackService ?? throw new ArgumentNullException($"{nameof(slackService)}"); ;
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
            var sender = _slackService.Users?[data.Event?.User];
            if (sender?.is_bot ?? true) return result;
            _slackService.ProceedReceviedMessage(sender.id, data.Event.Text);
            return result;
        }
    }
}
