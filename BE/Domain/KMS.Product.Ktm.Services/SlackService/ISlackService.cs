using SlackAPI;
using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Services.SlackService
{
    public interface ISlackService
    {
        SlackConfig Config { get; }
        TimeSpan ConnectionTimeout { get; }
        Dictionary<string, User> Users { get; }


        /// <summary>Sends the confirmation response.</summary>
        /// <param name="sender">The Slack sender.</param>
        /// <param name="originalMessage">The original message.</param>
        void SendConfirmationResponse(User sender, string originalMessage);


        /// <summary>Sends the inform messages.</summary>
        /// <param name="sender">The Slack sender.</param>
        /// <param name="originalMessage">The original message.</param>
        void SendInformMessages(User sender, string originalMessage);
    }
}