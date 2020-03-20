using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Api.Authentication
{
    // Customized authentication handler for KMS token verification
    public class KmsTokenAuthHandler : AuthenticationHandler<KmsTokenAuthOptions>
    {

        public KmsTokenAuthHandler(IOptionsMonitor<KmsTokenAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            HttpClient client = new HttpClient();
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://home.kms-technology.com/api/account/authenticate");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // not safe , just as an example , should custom claims on your own 
                var id = new ClaimsIdentity(new Claim[] { new Claim("Key", token) }, Scheme.Name);
                ClaimsPrincipal principal = new ClaimsPrincipal(id);
                var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Token is invalid");
            }
        }
    }
}
