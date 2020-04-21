using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KMS.Product.Ktm.Entities.Common;

namespace KMS.Product.Ktm.Services.AuthenticateService
{
    public class AuthenticateService : IAuthenticateService
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Inject system configuration
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticateService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Authenticate using system configuration in appsettings
        /// </summary>
        /// <returns></returns>
        public async Task<string> AuthenticateUsingConfiguration()
        {
            var userLogin = new UserLogin()
            {
                Username = Configuration.GetValue<string>("KmsInfo:Username"),
                Password = Configuration.GetValue<string>("KmsInfo:Password")
            };
            return await Authenticate(userLogin);
        }

        /// <summary>
        /// Authenticate using UserLogin parameters
        /// </summary>
        /// <returns></returns>
        public async Task<string> AuthenticateUsingParameters(UserLogin userLogin)
        {
            return await Authenticate(userLogin);
        }

        /// <summary>
        /// Authenticate through KMS API
        /// API: https://home.kms-technology.com/api/Account/login
        /// </summary>
        /// <returns>Token from KMS</returns>
        private async Task<string> Authenticate(UserLogin userLogin)
        {
            var token = String.Empty;
            // Initialize httpclient with token to send request to KMS HRM 
            var client = new HttpClient();
            var bodyContent = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Configuration.GetValue<string>("KmsInfo:AuthenticateRequestUrl"), bodyContent);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object and get the token
                var contentString = await response.Content.ReadAsStringAsync();
                var kmsLoginResponse = JsonConvert.DeserializeObject<KmsLoginResponse>(contentString);
                token = kmsLoginResponse.Token;
            }
            
            return token;
        }
    }
}
