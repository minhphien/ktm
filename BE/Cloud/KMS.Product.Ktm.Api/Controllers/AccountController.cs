using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using KMS.Product.Ktm.Api.Exceptions;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");
        }

        /// <summary>
        /// api/me
        /// Get user information by token through KMS API
        /// </summary>
        /// <returns>
        /// - Status OK 200 with user information
        /// - Status Unauthorized 401 if token expires or invalid token
        /// </returns>
        [HttpGet("me")]
        public async Task<IActionResult> GetUserInforAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(Configuration.GetValue<string>("KmsInfo:AuthenticateUrl"));
                if (response.StatusCode == HttpStatusCode.OK)
                {                    
                    return Ok(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return Unauthorized("Invalid token");
                }
            }
            catch(BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
