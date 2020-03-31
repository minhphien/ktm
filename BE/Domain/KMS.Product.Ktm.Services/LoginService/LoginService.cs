using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private IConfiguration _configuration { get; }

        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Login through KMS system
        /// POST https://home.kms-technology.com/api/Account/login
        /// </summary>
        /// <returns>Token from KMS</returns>
        public async Task<string> Login(UserLogin userLogin)
        {
            var token = "";
            // Initialize httpclient with token to send request to KMS HRM 
            var client = new HttpClient();
            var bodyContent = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");
            var requestUrl = "https://home.kms-technology.com/api/Account/login";
            var response = await client.PostAsync(requestUrl, bodyContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Convert response JSON to object and get the token
                var contentString = await response.Content.ReadAsStringAsync();
                var kmsLoginResponse = JsonConvert.DeserializeObject<KmsLoginResponse>(contentString);
                token.Concat(kmsLoginResponse.Token); 
            }
            return token;
        }

        /// <summary>
        /// Login with configuration from appsettings
        /// </summary>
        /// <returns></returns>
        public async Task<string> LoginWithConfiguration()
        {
            var userLogin = new UserLogin()
            {
                Username = _configuration.GetValue<string>("KmsLogin:Username"),
                Password = _configuration.GetValue<string>("KmsLogin:Password")
            };
            return await Login(userLogin);
        }

        /// <summary>
        /// Login with UserLogin parameters
        /// </summary>
        /// <returns></returns>
        public async Task<string> LoginWithParameters(UserLogin userLogin)
        {
            return await Login(userLogin);
        }

        /// <summary>
        /// Login with token from appsettings
        /// This is used for temporary testing only
        /// </summary>
        /// <returns></returns>
        public string LoginWithTokens()
        {
            return _configuration.GetValue<string>("KmsTestingToken");
        }
    }
}
