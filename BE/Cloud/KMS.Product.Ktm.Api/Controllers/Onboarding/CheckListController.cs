using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.CheckListService;

namespace KMS.Product.Ktm.Api.Controllers.Onboarding
{
    [Route("api/onboarding/[controller]")]
    [ApiController]
    public class CheckListController : ControllerBase
    {

        private readonly ICheckListService _checkListService;

        /// <summary>
        /// Inject CheckList service
        /// </summary>
        /// <returns></returns>
        public CheckListController(ICheckListService checkListService)
        {
            _checkListService = checkListService ?? throw new ArgumentNullException($"{nameof(checkListService)}");
        }

        /// <summary>
        /// Get all checkLists
        /// GET: api/onboarding/checkList
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all CheckLists        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCheckListsAsync()
        {
            try
            {
                var checkLists = await _checkListService.GetAllCheckListsAsync();
                return Ok(checkLists);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a checkList by id
        /// GET: api/onboarding/checkList/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a CheckList
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheckListByIdAsync(int id)
        {
            try
            {
                var checkList = await _checkListService.GetCheckListByIdAsync(id);
                return Ok(checkList);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new checkList
        /// POST: api/onboarding/checkList
        /// </summary>
        /// <param name="checkList">checkList object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateCheckListAsync(CheckList checkList)
        {
            try
            {
                await _checkListService.CreateCheckListAsync(checkList);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a CheckList
        /// PUT: api/onboarding/checkList
        /// </summary>
        /// <param name="checkList">checkList object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateCheckListAsync(CheckList checkList)
        {
            try
            {
                await _checkListService.UpdateCheckListAsync(checkList);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new checkList
        /// DELETE: api/onboarding/checkList
        /// </summary>
        /// <param name="checkList">checkList object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteCheckListAsync(CheckList checkList)
        {
            try
            {
                await _checkListService.DeleteCheckListAsync(checkList);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
