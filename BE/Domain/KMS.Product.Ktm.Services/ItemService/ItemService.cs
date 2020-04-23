using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// Inject Item repository
        /// </summary>
        /// <returns></returns>
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException($"{nameof(itemRepository)}");
        }

        /// <summary>
        /// Get all Items
        /// </summary>
        /// <returns>Returns a collection of all Items</returns>
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetItemsAsync();
        }

        /// <summary>
        /// Get Items by id
        /// </summary>
        /// <returns>Returns a Item by id</returns>
        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _itemRepository.GetByIdAsync(itemId);
        }

        /// <summary>
        /// Create a new Item
        /// </summary>
        /// <returns></returns>
        public async Task CreateItemAsync(Item item)
        {
            await _itemRepository.InsertAsync(item);
        }

        /// <summary>
        /// Update an existing Item
        /// </summary>
        /// <returns></returns>
        public async Task UpdateItemAsync(Item item)
        {
            await _itemRepository.UpdateAsync(item);
        }

        /// <summary>
        /// Delete an existing Item
        /// </summary>
        /// <returns></returns>
        public async Task DeleteItemAsync(Item item)
        {
            await _itemRepository.DeleteAsync(item);
        }
    }
}
