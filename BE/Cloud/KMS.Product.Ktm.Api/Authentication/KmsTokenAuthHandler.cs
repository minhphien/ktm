using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Services.AppConstants;

namespace KMS.Product.Ktm.Api.Authentication
{
    // Customized authentication handler for KMS token verification
    public class KmsTokenAuthHandler : AuthenticationHandler<KmsTokenAuthOptions>
    {
        private IConfiguration Configuration { get; }
        private readonly IMemoryCache _cache;

        public KmsTokenAuthHandler(
            IConfiguration configuration,
            IOptionsMonitor<KmsTokenAuthOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, 
            IMemoryCache cache)
            : base(options, logger, encoder, clock)
        {
            Configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");
            _cache = cache;
        }

        /// <summary>
        /// The customized authentication scheme
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            Console.WriteLine(token);
            string cacheEntry;
            var user = new KmsLoginResponse();

            // Check whether token in cache or not
            if (!_cache.TryGetValue(token, out _))
            {
                 user = await GetUserLogin(token);
                if (user != null)
                {
                    cacheEntry = token;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(Configuration.GetValue<int>("KmsInfo:CacheExpiration")));
                    // Save validated token into cache
                    _cache.Set(token, cacheEntry, cacheEntryOptions);
                    _cache.Set<string>(KudoConstants.UserInfo.USERNAME, user.UserName, cacheEntryOptions);
                    _cache.Set<string>(KudoConstants.UserInfo.NAME, user.ShortName, cacheEntryOptions);
                    _cache.Set<string>(KudoConstants.UserInfo.BADGEID, user.EmployeeCode, cacheEntryOptions);
                    _cache.Set<string>(KudoConstants.UserInfo.EMAIL, user.Email, cacheEntryOptions);

                    return AuthenticateResult.Success(CreateAuthTicket(token, user));
                }

                return AuthenticateResult.Fail("Token is invalid");
            }
            else
            {
                user.UserName = _cache.Get<string>(KudoConstants.UserInfo.USERNAME);
                user.ShortName = _cache.Get<string>(KudoConstants.UserInfo.NAME);
                user.EmployeeCode = _cache.Get<string>(KudoConstants.UserInfo.BADGEID);
                user.Email = _cache.Get<string>(KudoConstants.UserInfo.EMAIL);
            }

            return AuthenticateResult.Success(CreateAuthTicket(token, user));
        }

        /// <summary>
        /// Check whether token is valid or not
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<KmsLoginResponse> GetUserLogin(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(Configuration.GetValue<string>("KmsInfo:AuthenticateUrl"));

            if (response.StatusCode == HttpStatusCode.OK)
            {                
                return JsonConvert.DeserializeObject<KmsLoginResponse>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        /// <summary>
        /// Create an authentication ticket containing authentication claims when successfully authenticate via token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private AuthenticationTicket CreateAuthTicket(string token, KmsLoginResponse user)
        {
            var userData = new ClaimsIdentity(
                new Claim[] { 
                    new Claim(KudoConstants.UserInfo.KEY, token),
                    new Claim(KudoConstants.UserInfo.USERNAME, user.UserName),
                    new Claim(KudoConstants.UserInfo.NAME, user.ShortName),
                    new Claim(KudoConstants.UserInfo.BADGEID, user.EmployeeCode),
                    new Claim(KudoConstants.UserInfo.EMAIL, user.Email)
                }, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(userData);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name);
            return ticket;
        }
    }
}
