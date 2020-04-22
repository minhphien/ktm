using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.CheckListService
{
    public class CheckListService : ICheckListService
    {
        private readonly ICheckListRepository _checkListRepository;

        /// <summary>
        /// Inject CheckList repository
        /// </summary>
        /// <returns></returns>
        public CheckListService(ICheckListRepository checkListRepository)
        {
            _checkListRepository = checkListRepository ?? throw new ArgumentNullException($"{nameof(checkListRepository)}");
        }

        /// <summary>
        /// Get all CheckLists
        /// </summary>
        /// <returns>Returns a collection of all CheckLists</returns>
        public async Task<IEnumerable<CheckList>> GetAllCheckListsAsync()
        {
            return await _checkListRepository.GetCheckListsAsync();
        }

        /// <summary>
        /// Get CheckLists by id
        /// </summary>
        /// <returns>Returns a CheckList by id</returns>
        public async Task<CheckList> GetCheckListByIdAsync(int checkListId)
        {
            return await _checkListRepository.GetByIdAsync(checkListId);
        }

        /// <summary>
        /// Create a new CheckList
        /// </summary>
        /// <returns></returns>
        public async Task CreateCheckListAsync(CheckList checkList)
        {
            await _checkListRepository.InsertAsync(checkList);
        }

        /// <summary>
        /// Update an existing CheckList
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCheckListAsync(CheckList checkList)
        {
            await _checkListRepository.UpdateAsync(checkList);
        }

        /// <summary>
        /// Delete an existing CheckList
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCheckListAsync(CheckList checkList)
        {
            await _checkListRepository.DeleteAsync(checkList);
        }
    }
}
