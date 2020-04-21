using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Common;

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
    }
}
