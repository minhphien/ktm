using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.AuthenticateService
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Authenticate using system configuration in appsettings
        /// </summary>
        /// <returns></returns>
        Task<string> AuthenticateUsingConfiguration();

        /// <summary>
        /// Authenticate using UserLogin parameters
        /// </summary>
        /// <returns></returns>
        Task<string> AuthenticateUsingParameters(UserLogin userLogin);

        /// <summary>
        /// Authenticate using token from system configuration in appsettings
        /// This is used for temporary testing only
        /// </summary>
        /// <returns></returns>
        string AuthenticateUsingToken();
    }
}
