using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.LoginService
{
    public interface ILoginService
    {
        /// <summary>
        /// Login with configuration from appsettings
        /// </summary>
        /// <returns></returns>
        Task<string> LoginWithConfiguration();


        /// <summary>
        /// Login with UserLogin parameters
        /// </summary>
        /// <returns></returns>
        Task<string> LoginWithParameters(UserLogin userLogin);

        /// <summary>
        /// Login with token from appsettings
        /// This is used for temporary testing only
        /// </summary>
        /// <returns></returns>
        string LoginWithTokens();
    }
}
