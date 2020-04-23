using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Services.KudoService;
using KMS.Product.Ktm.Services.AppConstants;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IConfiguration Configuration { get; }
        private readonly IKudoService _kudoService;

        public AccountController(IConfiguration configuration, IKudoService kudoService)
        {
            Configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");
            _kudoService = kudoService ?? throw new ArgumentNullException($"{nameof(kudoService)}");
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
        public IActionResult GetUserInfor()
        {
            try
            {
                return Ok(new KmsLoginResponse
                {
                    UserName = User.FindFirst(KudoConstants.UserInfo.USERNAME)?.Value,
                    ShortName = User.FindFirst(KudoConstants.UserInfo.NAME)?.Value,
                    EmployeeCode = User.FindFirst(KudoConstants.UserInfo.BADGEID)?.Value,
                    Email = User.FindFirst(KudoConstants.UserInfo.EMAIL)?.Value
            });
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
