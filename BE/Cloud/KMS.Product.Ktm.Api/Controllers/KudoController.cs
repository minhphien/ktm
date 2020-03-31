using System;
using System.Threading.Tasks;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.KudoService;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.Api.Controllers
{
    public class KudoController : ControllerBase
    {

        private readonly IKudoService _kudoService;

        /// <summary>
        /// Inject Kudo service
        /// </summary>
        /// <returns></returns>
        public KudoController(IKudoService kudoService)
        {
            _kudoService = kudoService ?? throw new ArgumentNullException($"{nameof(kudoService)}");
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
        public async Task<IActionResult> GetAllKudosAsync()
        {
            try
            {
                var kudos = await _kudoService.GetAllKudosAsync();
                return Ok(kudos);
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a kudo by id
        /// GET: api/Kudo/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a kudo
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKudoByIdAsync(int id)
        {
            try
            {
                var Kudo = await _kudoService.GetKudoByIdAsync(id);
                return Ok(Kudo);
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new kudo
        /// POST: api/Kudo
        /// </summary>
        /// <param name="Kudo">Kudo object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateKudoAsync(Kudo Kudo)
        {
            try
            {
                await _kudoService.CreateKudoAsync(Kudo);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a kudo
        /// PUT: api/Kudo
        /// </summary>
        /// <param name="Kudo">Kudo object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateKudoAsync(Kudo Kudo)
        {
            try
            {
                await _kudoService.UpdateKudoAsync(Kudo);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new kudo
        /// DELETE: api/Kudo
        /// </summary>
        /// <param name="Kudo">Kudo object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteKudoAsync(Kudo Kudo)
        {
            try
            {
                await _kudoService.DeleteKudoAsync(Kudo);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
