using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Common;

namespace KMS.Product.Ktm.Services.EmailService
{
    public interface IEmailService
    {
        /// <summary>
        /// sending email
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        Task SendEmail(EmailMessage emailMessage);

        /// <summary>
        /// get emails by max count
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        Task<List<EmailMessage>> GetEmails(int maxCount = 10);
    }
}
