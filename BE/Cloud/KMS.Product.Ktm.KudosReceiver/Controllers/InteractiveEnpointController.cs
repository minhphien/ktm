using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.KudosReceiver.Controllers
{
    [Route("slack/[controller]/[action]")]
    [ApiController]
    public class InteractiveEnpointController : ControllerBase
    {
        // GET: slack/InteractiveEnpoint
        [HttpGet]
        [ActionName("Index")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: slack/InteractiveEnpoint
        [HttpPost]
        [ActionName("Index")]
        public string Post([FromBody] dynamic value)
        {
            var slackClient = SlackClient.SlackFixture.Instance().UserClient;
            slackClient.PostMessage(response =>
            {
            }, "#test-kmskudos", $"someone post {value?.ToString()}");
            return value?.ToString();
        }

        [HttpPost]
        [ActionName("send_gift")]
        public string SendGift([FromBody] dynamic value)
        {
            return Post(value);
        }
    }
}
