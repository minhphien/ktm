using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KudoTypeController : ControllerBase
    {
        private readonly IKudoTypeService _kudoTypeService;

        public KudoTypeController(IKudoTypeService kudoTypeService)
        {
            _kudoTypeService = kudoTypeService;
        }

        // GET: api/KudoType
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var kudoTypes = _kudoTypeService.GetAllKudoTypes();
                return Ok(kudoTypes);
            }
            catch(BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/KudoType/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var kudoType = _kudoTypeService.GetKudoTypeById(id);
                return Ok(kudoType);
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/KudoType
        [HttpPost]
        public IActionResult Post(KudoType kudoType)
        {
            try
            {
                _kudoTypeService.CreateKudoType(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/KudoType
        [HttpPut]
        public IActionResult Put(KudoType kudoType)
        {
            try
            {
                _kudoTypeService.UpdateKudoType(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/KudoType
        [HttpDelete]
        public IActionResult Delete(KudoType kudoType)
        {
            try
            {
                _kudoTypeService.DeleteKudoType(kudoType);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
