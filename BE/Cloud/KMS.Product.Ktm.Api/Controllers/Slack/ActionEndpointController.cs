using KMS.Product.Ktm.Api.Models.Events;
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
        // GET: slack/InteractiveEnpoint
        [HttpGet]
        public SlackEvent Get()
        {
            return new SlackEvent { Challenge = "OK" };
        }

        // POST: slack/InteractiveEnpoint
        [HttpPost]
        public string Body([FromBody] SlackEvent data)
        {
            var result = data?.Challenge;
            var sender =  slackService.Users?[data?.Event.User];
            if (sender?.is_bot ?? true) return result;
            slackService.SendConfirmationResponse(sender, data?.Event.Text);
            slackService.SendInformMessages(sender, data?.Event.Text);
            return result;
        }
    }
}
