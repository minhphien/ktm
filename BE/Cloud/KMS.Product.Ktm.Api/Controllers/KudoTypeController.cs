using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.KudoTypeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KudoTypeController : ControllerBase
    {
        private readonly IKudoTypeService _kudoTypeService;

        /// <summary>
        /// Inject KudoType service
        /// </summary>
        /// <returns></returns>
        public KudoTypeController(IKudoTypeService kudoTypeService)
        {
            _kudoTypeService = kudoTypeService ?? throw new ArgumentNullException($"{nameof(kudoTypeService)}");
        }

        /// <summary>
        /// Get all kudo types
        /// GET: api/KudoType
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all kudo types        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllKudoTypesAsync()
        {
            try
            {
                var kudoTypes = await _kudoTypeService.GetAllKudoTypesAsync();
                return Ok(kudoTypes);
            }
            catch(BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a kudo type by id
        /// GET: api/KudoType/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a kudo type 
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKudoTypeByIdAsync(int id)
        {
            try
            {
                var kudoType = await _kudoTypeService.GetKudoTypeByIdAsync(id);
                return Ok(kudoType);
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new kudo type
        /// POST: api/KudoType
        /// </summary>
        /// <param name="kudoType">KudoType object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateKudoTypeAsync(KudoType kudoType)
        {
            try
            {
                await _kudoTypeService.CreateKudoTypeAsync(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a kudo type
        /// PUT: api/KudoType
        /// </summary>
        /// <param name="kudoType">KudoType object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateKudoTypeAsync(KudoType kudoType)
        {
            try
            {
                await _kudoTypeService.UpdateKudoTypeAsync(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new kudo type
        /// DELETE: api/KudoType
        /// </summary>
        /// <param name="kudoType">KudoType object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteKudoTypeAsync(KudoType kudoType)
        {
            try
            {
                await _kudoTypeService.DeleteKudoTypeAsync(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
