using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.ItemService
{
    public interface IItemService
    {
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>Returns a collection of all items</returns>
        Task<IEnumerable<Item>> GetAllItemsAsync();

        /// <summary>
        /// Get items by id
        /// </summary>
        /// <returns>Returns a item by id</returns>
        Task<Item> GetItemByIdAsync(int itemId);

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <returns></returns>
        Task CreateItemAsync(Item item);

        /// <summary>
        /// Update an existing item
        /// </summary>
        /// <returns></returns>
        Task UpdateItemAsync(Item item);

        /// <summary>
        /// Delete an existing item
        /// </summary>
        /// <returns></returns>
        Task DeleteItemAsync(Item item);
    }
}

