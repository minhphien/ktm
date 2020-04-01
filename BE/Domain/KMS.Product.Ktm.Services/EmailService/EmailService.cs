using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using MailKit;
using MailKit.Search;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Entities.Configurations;

namespace KMS.Product.Ktm.Services.EmailService
{
    public class EmailService : IEmailService
    {
		private readonly IEmailConfiguration _emailConfiguration;
		private readonly SmtpClient sendClient;
		private readonly ImapClient getClient;

		public EmailService(IEmailConfiguration emailConfiguration)
		{
			_emailConfiguration = emailConfiguration;
			sendClient = new SmtpClient(new ProtocolLogger(Console.OpenStandardError()));
			getClient = new ImapClient(new ProtocolLogger(Console.OpenStandardError()));
		}

		/// <summary>
		/// sending email
		/// </summary>
		/// <param name="emailMessage"></param>
		/// <returns></returns>
		public async Task SendEmail(EmailMessage emailMessage)
		{
			var message = new MimeMessage();
			message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
			message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

			message.Subject = emailMessage.Subject;
			message.Body = new TextPart(TextFormat.Html)
			{
				Text = emailMessage.Content
			};

			await sendClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort);
			await sendClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
			await sendClient.SendAsync(message);
			await sendClient.DisconnectAsync(true);
		}

		/// <summary>
		/// get emails by max count
		/// </summary>
		/// <param name="maxCount"></param>
		/// <returns></returns>
		public async Task<List<EmailMessage>> GetEmails(int maxCount = 10)
		{

			await getClient.ConnectAsync(_emailConfiguration.ImapServer, _emailConfiguration.ImapPort, true);
			await getClient.AuthenticateAsync(_emailConfiguration.ImapUsername, _emailConfiguration.ImapPassword);
			await getClient.Inbox.OpenAsync(FolderAccess.ReadOnly);

			var uids = await getClient.Inbox.SearchAsync(SearchQuery.All);
			List<EmailMessage> emails = new List<EmailMessage>();

			for (int i = 0; i < maxCount; i++)
			{
				var message = await getClient.Inbox.GetMessageAsync(uids[i]);
				var emailMessage = new EmailMessage
				{
					Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
					Subject = message.Subject
				};
				emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
				emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
				emails.Add(emailMessage);
			}

			await getClient.DisconnectAsync(true);

			return emails;
		}
	}
}
