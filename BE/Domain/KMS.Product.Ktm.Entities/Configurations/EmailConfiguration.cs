using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Configurations
{
	public class EmailConfiguration : IEmailConfiguration
	{
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }

		public string ImapServer { get; set; }
		public int ImapPort { get; set; }
		public string ImapUsername { get; set; }
		public string ImapPassword { get; set; }
	}

	public interface IEmailConfiguration
	{
		string SmtpServer { get; }
		int SmtpPort { get; }
		string SmtpUsername { get; set; }
		string SmtpPassword { get; set; }

		string ImapServer { get; }
		int ImapPort { get; }
		string ImapUsername { get; }
		string ImapPassword { get; }
	}
}
