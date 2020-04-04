using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.DTO;
using KMS.Product.Ktm.Services.KudoService;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            _mapper = mapper;
        }

        /// <summary>
        /// Get all kudos
        /// GET: api/Kudo
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all kudos        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetKudosForReport(
            [FromQuery] DateTime? dateFrom, 
            [FromQuery] DateTime? dateTo,
            [FromQuery] List<int> teamIds,
            [FromQuery] List<int> kudoTypeIds)
        {
            try
            {
                IEnumerable<KudoReportDto> kudos = await _kudoService.GetKudosForReport(dateFrom, dateTo, teamIds, kudoTypeIds);
                return Ok(kudos);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
