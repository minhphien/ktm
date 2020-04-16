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
            string cacheEntry;          

            // Check whether token in cache or not
            if (!_cache.TryGetValue(token, out _))
            {
                if (await IsTokenValid(token))
                {
                    cacheEntry = token;
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(Configuration.GetValue<int>("KmsInfo:CacheExpiration")));
                    // Save validated token into cache
                    _cache.Set(token, cacheEntry, cacheEntryOptions);
                    return AuthenticateResult.Success(CreateAuthTicket(token));
                }

                return AuthenticateResult.Fail("Token is invalid");
            }

            return AuthenticateResult.Success(CreateAuthTicket(token));
        }

        /// <summary>
        /// Check whether token is valid or not
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> IsTokenValid(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(Configuration.GetValue<string>("KmsInfo:AuthenticateUrl"));

            if (response.StatusCode == HttpStatusCode.OK)
            {                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create an authentication ticket containing authentication claims when successfully authenticate via token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private AuthenticationTicket CreateAuthTicket(string token)
        {
            var id = new ClaimsIdentity(new Claim[] { new Claim("Key", token) }, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(id);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name);
            return ticket;
        }
    }
}
