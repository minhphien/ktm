using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;

namespace KMS.Product.Ktm.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody]Employee employee)
        {
            try
            {
                _service.CreateEmployee(employee);
                return Ok("Success");
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
