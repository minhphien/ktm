using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.CheckListItemService;

namespace KMS.Product.Ktm.Api.Controllers.Onboarding
{
    [Route("api/onboarding/[controller]")]
    [ApiController]
    public class CheckListItemController : ControllerBase
    {

        private readonly ICheckListItemService _checkListItemService;

        /// <summary>
        /// Inject CheckListItem service
        /// </summary>
        /// <returns></returns>
        public CheckListItemController(ICheckListItemService checkListItemService)
        {
            _checkListItemService = checkListItemService ?? throw new ArgumentNullException($"{nameof(checkListItemService)}");
        }

        /// <summary>
        /// Get all checkListItems
        /// GET: api/onboarding/checkListItem
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all CheckListItems        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCheckListItemsAsync()
        {
            try
            {
                var checkListItems = await _checkListItemService.GetAllCheckListItemsAsync();
                return Ok(checkListItems);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a checkListItem by id
        /// GET: api/onboarding/checkListItem/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a CheckListItem
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckListItemByIdAsync(int id)
        {
            try
            {
                var checkListItem = await _checkListItemService.GetCheckListItemByIdAsync(id);
                return Ok(checkListItem);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new checkListItem
        /// POST: api/onboarding/checkListItem
        /// </summary>
        /// <param name="checkListItem">checkListItem object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateCheckListItemAsync(CheckListItem checkListItem)
        {
            try
            {
                await _checkListItemService.CreateCheckListItemAsync(checkListItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a CheckListItem
        /// PUT: api/onboarding/checkListItem
        /// </summary>
        /// <param name="checkListItem">checkListItem object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateCheckListItemAsync(CheckListItem checkListItem)
        {
            try
            {
                await _checkListItemService.UpdateCheckListItemAsync(checkListItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new checkListItem
        /// DELETE: api/onboarding/checkListItem
        /// </summary>
        /// <param name="checkListItem">checkListItem object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteCheckListItemAsync(CheckListItem checkListItem)
        {
            try
            {
                await _checkListItemService.DeleteCheckListItemAsync(checkListItem);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
