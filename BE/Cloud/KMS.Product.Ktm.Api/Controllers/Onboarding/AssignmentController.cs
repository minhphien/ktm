using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.AssignmentService;

namespace KMS.Product.Ktm.Api.Controllers.Onboarding
{
    [Route("api/onboarding/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentController : ControllerBase
    {

        private readonly IAssignmentService _assignmentService;

        /// <summary>
        /// Inject Assignment service
        /// </summary>
        /// <returns></returns>
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService ?? throw new ArgumentNullException($"{nameof(assignmentService)}");
        }

        /// <summary>
        /// Get all assignments
        /// GET: api/onboarding/assignment
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all Assignments        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAssignmentsAsync()
        {
            try
            {
                var assignments = await _assignmentService.GetAllAssignmentsAsync();
                return Ok(assignments);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a assignment by id
        /// GET: api/onboarding/assignment/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a Assignment
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentByIdAsync(int id)
        {
            try
            {
                var assignment = await _assignmentService.GetAssignmentByIdAsync(id);
                return Ok(assignment);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new assignment
        /// POST: api/onboarding/assignment
        /// </summary>
        /// <param name="assignment">assignment object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAssignmentAsync(Assignment assignment)
        {
            try
            {
                await _assignmentService.CreateAssignmentAsync(assignment);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a Assignment
        /// PUT: api/onboarding/assignment
        /// </summary>
        /// <param name="assignment">assignment object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateAssignmentAsync(Assignment assignment)
        {
            try
            {
                await _assignmentService.UpdateAssignmentAsync(assignment);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new assignment
        /// DELETE: api/onboarding/assignment
        /// </summary>
        /// <param name="assignment">assignment object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignmentAsync(Assignment assignment)
        {
            try
            {
                await _assignmentService.DeleteAssignmentAsync(assignment);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
