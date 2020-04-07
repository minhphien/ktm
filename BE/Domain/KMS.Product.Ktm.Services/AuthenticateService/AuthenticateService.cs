using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.AuthenticateService
{
    public class AuthenticateService : IAuthenticateService
    {
        private IConfiguration _configuration { get; }

        public const string AuthenticateRequestUrl = "https://home.kms-technology.com/api/Account/login";

        /// <summary>
        /// Inject system configuration
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticate using system configuration in appsettings
        /// </summary>
        /// <returns></returns>
        public async Task<string> AuthenticateUsingConfiguration()
        {
            var userLogin = new UserLogin()
            {
                Username = _configuration.GetValue<string>("KmsLogin:Username"),
                Password = _configuration.GetValue<string>("KmsLogin:Password")
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
        /// Authenticate using token from system configuration in appsettings
        /// This is used for temporary testing only
        /// </summary>
        /// <returns></returns>
        public string AuthenticateUsingToken()
        {
            return _configuration.GetValue<string>("KmsTestingToken");
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
            var response = await client.PostAsync(AuthenticateRequestUrl, bodyContent);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object and get the token
                var contentString = await response.Content.ReadAsStringAsync();
                var kmsLoginResponse = JsonConvert.DeserializeObject<KmsLoginResponse>(contentString);
                token.Concat(kmsLoginResponse.Token);
            }
            
            return token;
        }
    }
}
