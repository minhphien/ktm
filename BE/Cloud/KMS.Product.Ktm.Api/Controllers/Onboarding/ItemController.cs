using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KMS.Product.Ktm.Api.Exceptions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.ItemService;

namespace KMS.Product.Ktm.Api.Controllers.Onboarding
{
    [Route("api/onboarding/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        /// <summary>
        /// Inject Item service
        /// </summary>
        /// <returns></returns>
        public ItemController(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException($"{nameof(itemService)}");
        }

        /// <summary>
        /// Get all items
        /// GET: api/onboarding/item
        /// </summary>
        /// <returns>
        /// Success: returns 200 status code with a collection of all Items        
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();
                return Ok(items);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a item by id
        /// GET: api/onboarding/item/id
        /// </summary>
        /// <param name="id">query string</param>
        /// <returns>
        /// Success: returns 200 status code with a Item
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemByIdAsync(int id)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(id);
                return Ok(item);
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new item
        /// POST: api/onboarding/item
        /// </summary>
        /// <param name="item">Item object body request</param>
        /// <returns>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateItemAsync(Item item)
        {
            try
            {
                await _itemService.CreateItemAsync(item);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a Item
        /// PUT: api/onboarding/item
        /// </summary>
        /// <param name="item">Item object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpPut]
        public async Task<IActionResult> UpdateItemAsync(Item item)
        {
            try
            {
                await _itemService.UpdateItemAsync(item);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete new item
        /// DELETE: api/onboarding/item
        /// </summary>
        /// <param name="item">Item object body request</param>
        /// Success: returns 200 status code
        /// Failure: returns 500 status code with an exception message
        [HttpDelete]
        public async Task<IActionResult> DeleteItemAsync(Item item)
        {
            try
            {
                await _itemService.DeleteItemAsync(item);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
