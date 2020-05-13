using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.DTO;
using KMS.Product.Ktm.Services.KudoService;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportController : ControllerBase
    {

        private readonly IKudoService _kudoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inject Kudo service
        /// </summary>
        /// <returns></returns>
        public ReportController(IKudoService kudoService, IMapper mapper)
        {
            _kudoService = kudoService ?? throw new ArgumentNullException($"{nameof(kudoService)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
        }

        /// <summary>
        /// Get kudos dto for report
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all kudos        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public IActionResult GetKudosByTeamForReport(
            DateTime? dateFrom, 
            DateTime? dateTo,
            int teamIds,
            int kudoTypeIds)
        {
            try
            {
                var kudos = _kudoService.GetKudosForReport(dateFrom, dateTo,
                    new List<int>() { teamIds }, new List<int>() { kudoTypeIds }).Result;
                    
                return Ok(kudos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get kudos summary for report
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all kudos        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("summary")]
        public async Task<IActionResult> GetKudosummaryForReport(
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] List<int> filterIds,
            [FromQuery] int type)
        {
            try
            {
                IEnumerable<KudoSumReportDto> kudos = await _kudoService.GetKudosummaryForReport(dateFrom, dateTo, filterIds, type);
                return Ok(kudos);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>Gets the received kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="teamIds">The team ids.</param>
        /// <param name="kudoTypeIds">The kudo type ids.</param>
        /// <returns></returns>
        [HttpGet("receive/{badgeId}")]
        public async Task<IActionResult> GetReceivedKudosByBadgeId( string badgeId,
           [FromQuery] DateTime? dateFrom,
           [FromQuery] DateTime? dateTo,
           [FromQuery] int kudoTypeIds)
        {
            try
            {
                var kudos = await _kudoService.GetReceivedKudosByBadgeId(badgeId, dateFrom, dateTo, new List<int>() { kudoTypeIds });
                return Ok(kudos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>Gets the sent kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="kudoTypeIds">The kudo type ids.</param>
        /// <returns></returns>
        [HttpGet("send/{badgeId}")]
        public async Task<IActionResult> GetSentKudosByBadgeId(
            string badgeId, 
            DateTime? dateFrom,
            DateTime? dateTo,
            int kudoTypeIds)
        {
            try
            {
                var kudos = await _kudoService.GetSentKudosByBadgeId(badgeId, dateFrom, dateTo, new List<int>() { kudoTypeIds });
                return Ok(kudos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
