using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Common
{
	/// <summary>
	/// email message class contain email addresses 
	/// </summary>
    public class EmailMessage
    {
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddresses = new List<EmailAddress>();
		}

		// list sender addresses
		public List<EmailAddress> ToAddresses { get; set; }
		// list receiver addresses
		public List<EmailAddress> FromAddresses { get; set; }
		// email message subject
		public string Subject { get; set; }
		// email message content detail
		public string Content { get; set; }
	}
}
