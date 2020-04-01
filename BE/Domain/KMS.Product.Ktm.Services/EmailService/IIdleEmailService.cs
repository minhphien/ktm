using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

namespace KMS.Product.Ktm.Services.EmailService
{
    public interface IIdleEmailService
    {
        /// <summary>
        /// run imap idle
        /// </summary>
        /// <returns></returns>
        Task RunAsync();

        /// <summary>
        /// exit
        /// </summary>
        void Exit();

        /// <summary>
        /// dispose
        /// </summary>
        void Dispose();
    }
}
