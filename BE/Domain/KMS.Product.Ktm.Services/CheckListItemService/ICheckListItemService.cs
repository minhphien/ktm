using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.CheckListItemService
{
    public interface ICheckListItemService
    {
        /// <summary>
        /// Get all checkListItems
        /// </summary>
        /// <returns>Returns a collection of all checkListItems</returns>
        Task<IEnumerable<CheckListItem>> GetAllCheckListItemsAsync();

        /// <summary>
        /// Get checkListItems by id
        /// </summary>
        /// <returns>Returns a checkListItem by id</returns>
        Task<CheckListItem> GetCheckListItemByIdAsync(int checkListItemId);

        /// <summary>
        /// Create a new checkListItem
        /// </summary>
        /// <returns></returns>
        Task CreateCheckListItemAsync(CheckListItem checkListItem);

        /// <summary>
        /// Update an existing checkListItem
        /// </summary>
        /// <returns></returns>
        Task UpdateCheckListItemAsync(CheckListItem checkListItem);

        /// <summary>
        /// Delete an existing checkListItem
        /// </summary>
        /// <returns></returns>
        Task DeleteCheckListItemAsync(CheckListItem checkListItem);
    }
}

