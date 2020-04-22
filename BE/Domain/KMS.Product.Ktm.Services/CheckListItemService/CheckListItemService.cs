using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.CheckListItemService
{
    public class CheckListItemService : ICheckListItemService
    {
        private readonly ICheckListItemRepository _checkListItemRepository;

        /// <summary>
        /// Inject CheckListItem repository
        /// </summary>
        /// <returns></returns>
        public CheckListItemService(ICheckListItemRepository checkListItemRepository)
        {
            _checkListItemRepository = checkListItemRepository ?? throw new ArgumentNullException($"{nameof(checkListItemRepository)}");
        }

        /// <summary>
        /// Get all CheckListItems
        /// </summary>
        /// <returns>Returns a collection of all CheckListItems</returns>
        public async Task<IEnumerable<CheckListItem>> GetAllCheckListItemsAsync()
        {
            return await _checkListItemRepository.GetCheckListItemsAsync();
        }

        /// <summary>
        /// Get CheckListItems by id
        /// </summary>
        /// <returns>Returns a CheckListItem by id</returns>
        public async Task<CheckListItem> GetCheckListItemByIdAsync(int checkListItemId)
        {
            return await _checkListItemRepository.GetByIdAsync(checkListItemId);
        }

        /// <summary>
        /// Create a new CheckListItem
        /// </summary>
        /// <returns></returns>
        public async Task CreateCheckListItemAsync(CheckListItem checkListItem)
        {
            await _checkListItemRepository.InsertAsync(checkListItem);
        }

        /// <summary>
        /// Update an existing CheckListItem
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCheckListItemAsync(CheckListItem checkListItem)
        {
            await _checkListItemRepository.UpdateAsync(checkListItem);
        }

        /// <summary>
        /// Delete an existing CheckListItem
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCheckListItemAsync(CheckListItem checkListItem)
        {
            await _checkListItemRepository.DeleteAsync(checkListItem);
        }
    }
}
