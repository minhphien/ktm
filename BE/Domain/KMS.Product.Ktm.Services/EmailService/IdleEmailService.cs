using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;
using KMS.Product.Ktm.Entities.Configurations;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Services.KudoService;

namespace KMS.Product.Ktm.Services.EmailService
{
	public class IdleEmailService : IIdleEmailService, IDisposable
	{
		private readonly IEmailConfiguration _emailConfiguration;
		private readonly IKudoService _kudoService;
		private readonly SecureSocketOptions sslOptions;
		private readonly List<IMessageSummary> messages;
		private readonly CancellationTokenSource cancel;
		private readonly ImapClient client;
		private CancellationTokenSource done;
		private bool messagesArrived;

		public IdleEmailService(IEmailConfiguration emailConfiguration, IKudoService kudoService)
		{
			_emailConfiguration = emailConfiguration;
			_kudoService = kudoService;
			sslOptions = SecureSocketOptions.Auto;
			messages = new List<IMessageSummary>();
			cancel = new CancellationTokenSource();
			client = new ImapClient(new ProtocolLogger(Console.OpenStandardError()));
		}

		/// <summary>
		/// run imap idle
		/// </summary>
		/// <returns></returns>
		public async Task RunAsync()
		{
			// connect to the IMAP server and get our initial list of messages
			try
			{
				await ReconnectAsync();
				await FetchMessageSummariesAsync();
			}
			catch (OperationCanceledException)
			{
				await client.DisconnectAsync(true);
				return;
			}

			var inbox = client.Inbox;

			inbox.CountChanged += OnCountChanged;
			inbox.MessageExpunged += OnMessageExpunged;
			inbox.MessageFlagsChanged += OnMessageFlagsChanged;

			await IdleAsync();

			inbox.MessageFlagsChanged -= OnMessageFlagsChanged;
			inbox.MessageExpunged -= OnMessageExpunged;
			inbox.CountChanged -= OnCountChanged;

			await client.DisconnectAsync(true);
		}

		/// <summary>
		/// reconnect imap
		/// </summary>
		/// <returns></returns>
		async Task ReconnectAsync()
		{
			if (!client.IsConnected)
				await client.ConnectAsync(_emailConfiguration.ImapServer, _emailConfiguration.ImapPort, sslOptions, cancel.Token);

			if (!client.IsAuthenticated)
			{
				await client.AuthenticateAsync(_emailConfiguration.ImapUsername, _emailConfiguration.ImapPassword, cancel.Token);

				await client.Inbox.OpenAsync(FolderAccess.ReadOnly, cancel.Token);
			}
		}

		/// <summary>
		/// fetch message summaries
		/// </summary>
		/// <returns></returns>
		async Task FetchMessageSummariesAsync()
		{
			IList<IMessageSummary> fetched;
			bool firstTime = messages.Count == 0;

			do
			{
				try
				{
					// fetch summary information for messages that we don't already have
					int startIndex = messages.Count;

					fetched = client.Inbox.Fetch(startIndex, -1, MessageSummaryItems.Full | MessageSummaryItems.UniqueId, cancel.Token);
					break;
				}
				catch (ImapProtocolException)
				{
					// protocol exceptions often result in the client getting disconnected
					await ReconnectAsync();
				}
				catch (IOException)
				{
					// I/O exceptions always result in the client getting disconnected
					await ReconnectAsync();
				}
			} while (true);

			var emails = new List<EmailMessage>();
			foreach (var message in fetched)
			{
				if (!firstTime)
				{
					var detail = await client.Inbox.GetMessageAsync(message.Index);
					var emailMessage = new EmailMessage
					{
						Content = !string.IsNullOrEmpty(detail.HtmlBody) ? detail.HtmlBody : detail.TextBody,
						Subject = detail.Subject
					};
					emailMessage.ToAddresses.AddRange(detail.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
					emailMessage.FromAddresses.AddRange(detail.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
					emails.Add(emailMessage);
				}
				messages.Add(message);
			}
			
			if(emails.Count > 0)
			{
				await _kudoService.InsertKudoFromEmails(emails);
			}
		}

		/// <summary>
		/// wait for new message
		/// </summary>
		/// <returns></returns>
		async Task WaitForNewMessagesAsync()
		{
			do
			{
				try
				{
					if (client.Capabilities.HasFlag(ImapCapabilities.Idle))
					{
						// GMail drops idle connections after about 10 minutes, so we'll only idle for 9 minutes.
						using (done = new CancellationTokenSource(new TimeSpan(0, 9, 0)))
							await client.IdleAsync(done.Token, cancel.Token);
					}
					else
					{
						// wait a minute between each NOOP command.
						await Task.Delay(new TimeSpan(0, 1, 0), cancel.Token);
						await client.NoOpAsync(cancel.Token);
					}
					break;
				}
				catch (ImapProtocolException)
				{
					// protocol exceptions often result in the client getting disconnected
					await ReconnectAsync();
				}
				catch (IOException)
				{
					// I/O exceptions always result in the client getting disconnected
					await ReconnectAsync();
				}
			} while (true);
		}

		/// <summary>
		/// idle wait for new message
		/// </summary>
		/// <returns></returns>
		async Task IdleAsync()
		{
			do
			{
				try
				{
					await WaitForNewMessagesAsync();

					if (messagesArrived)
					{
						await FetchMessageSummariesAsync();
						messagesArrived = false;
					}
				}
				catch (OperationCanceledException)
				{
					break;
				}
			} while (!cancel.IsCancellationRequested);
		}

		/// <summary>
		/// event will fire when new messages arrive in the folder and/or when messages are expunged.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnCountChanged(object sender, EventArgs e)
		{
			var folder = (ImapFolder)sender;

			if (folder.Count > messages.Count)
			{
				messagesArrived = true;
				done?.Cancel();
			}
		}

		/// <summary>
		/// when message expunge
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMessageExpunged(object sender, MessageEventArgs e)
		{
			if (e.Index < messages.Count)
			{
				messages.RemoveAt(e.Index);
			}
		}

		/// <summary>
		/// when message flag change
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnMessageFlagsChanged(object sender, MessageFlagsChangedEventArgs e){ }

		/// <summary>
		/// exit
		/// </summary>
		public void Exit()
		{
			cancel.Cancel();
		}

		/// <summary>
		/// dispose
		/// </summary>
		public void Dispose()
		{
			client.Dispose();
			cancel.Dispose();
			done?.Dispose();
		}
	}
}

