using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.AssignmentItemService;

namespace KMS.Product.Ktm.Api.Controllers.Onboarding
{
    [Route("api/onboarding/[controller]")]
    [ApiController]
    [Authorize]
    public class AssignmentItemController : ControllerBase
    {

        private readonly IAssignmentItemService _assignmentItemService;

        /// <summary>
        /// Inject AssignmentItem service
        /// </summary>
        /// <returns></returns>
        public AssignmentItemController(IAssignmentItemService assignmentItemService)
        {
            _assignmentItemService = assignmentItemService ?? throw new ArgumentNullException($"{nameof(assignmentItemService)}");
        }

        /// <summary>
        /// Get all assignmentItems
        /// GET: api/onboarding/assignmentItem
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all AssignmentItems        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAssignmentItemsAsync()
        {
            try
            {
                var assignmentItems = await _assignmentItemService.GetAllAssignmentItemsAsync();
                return Ok(assignmentItems);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a assignmentItem by id
        /// GET: api/onboarding/assignmentItem/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a AssignmentItem
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentItemByIdAsync(int id)
        {
            try
            {
                var assignmentItem = await _assignmentItemService.GetAssignmentItemByIdAsync(id);
                return Ok(assignmentItem);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new assignmentItem
        /// POST: api/onboarding/assignmentItem
        /// </summary>
        /// <param name="assignmentItem">assignmentItem object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            try
            {
                await _assignmentItemService.CreateAssignmentItemAsync(assignmentItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a AssignmentItem
        /// PUT: api/onboarding/assignmentItem
        /// </summary>
        /// <param name="assignmentItem">assignmentItem object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            try
            {
                await _assignmentItemService.UpdateAssignmentItemAsync(assignmentItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new assignmentItem
        /// DELETE: api/onboarding/assignmentItem
        /// </summary>
        /// <param name="assignmentItem">assignmentItem object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            try
            {
                await _assignmentItemService.DeleteAssignmentItemAsync(assignmentItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
