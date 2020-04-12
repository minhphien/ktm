using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
//using KMS.Product.Ktm.Services.EmployeeService;
//using KMS.Product.Ktm.Services.TeamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {
        }

        [HttpGet("me")]
        public async Task<IActionResult> AccountUserAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync("https://home.kms-technology.com/api/account/authenticate");
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
