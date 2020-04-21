using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.CheckListService
{
    public interface ICheckListService
    {
        /// <summary>
        /// Get all checkLists
        /// </summary>
        /// <returns>Returns a collection of all checkLists</returns>
        Task<IEnumerable<CheckList>> GetAllCheckListsAsync();

        /// <summary>
        /// Get checkLists by id
        /// </summary>
        /// <returns>Returns a checkList by id</returns>
        Task<CheckList> GetCheckListByIdAsync(int checkListId);

        /// <summary>
        /// Create a new checkList
        /// </summary>
        /// <returns></returns>
        Task CreateCheckListAsync(CheckList checkList);

        /// <summary>
        /// Update an existing checkList
        /// </summary>
        /// <returns></returns>
        Task UpdateCheckListAsync(CheckList checkList);

        /// <summary>
        /// Delete an existing checkList
        /// </summary>
        /// <returns></returns>
        Task DeleteCheckListAsync(CheckList checkList);
    }
}

