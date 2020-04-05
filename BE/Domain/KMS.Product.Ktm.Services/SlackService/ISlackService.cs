using SlackAPI;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Services.SlackService
{
    public interface ISlackService
    {
        Dictionary<string, User> Users { get; }
        /// <summary>Proceeds the recevied message.</summary>
        /// <param name="slackUserId">The slack user ID.</param>
        /// <param name="message">The message.</param>
        void ProceedReceviedMessage(string slackUserId, string message);
    }
}