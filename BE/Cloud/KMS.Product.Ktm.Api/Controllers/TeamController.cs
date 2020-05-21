using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.TeamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMS.Product.Ktm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        // GET: api/Team
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _teamService.GetAllTeamsAsync();
            return Ok(result);
        }

        // GET: api/Team/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Team
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
