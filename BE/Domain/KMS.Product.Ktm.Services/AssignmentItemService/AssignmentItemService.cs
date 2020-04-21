using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.AssignmentItemService
{
    public class AssignmentItemService : IAssignmentItemService
    {
        private readonly IAssignmentItemRepository _assignmentItemRepository;

        /// <summary>
        /// Inject AssignmentItem repository
        /// </summary>
        /// <returns></returns>
        public AssignmentItemService(IAssignmentItemRepository assignmentItemRepository)
        {
            _assignmentItemRepository = assignmentItemRepository ?? throw new ArgumentNullException($"{nameof(assignmentItemRepository)}");
        }

        /// <summary>
        /// Get all AssignmentItems
        /// </summary>
        /// <returns>Returns a collection of all AssignmentItems</returns>
        public async Task<IEnumerable<AssignmentItem>> GetAllAssignmentItemsAsync()
        {
            return await _assignmentItemRepository.GetAssignmentItemsAsync();
        }

        /// <summary>
        /// Get AssignmentItems by id
        /// </summary>
        /// <returns>Returns a AssignmentItem by id</returns>
        public async Task<AssignmentItem> GetAssignmentItemByIdAsync(int assignmentItemId)
        {
            return await _assignmentItemRepository.GetByIdAsync(assignmentItemId);
        }

        /// <summary>
        /// Create a new AssignmentItem
        /// </summary>
        /// <returns></returns>
        public async Task CreateAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            await _assignmentItemRepository.InsertAsync(assignmentItem);
        }

        /// <summary>
        /// Update an existing AssignmentItem
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            await _assignmentItemRepository.UpdateAsync(assignmentItem);
        }

        /// <summary>
        /// Delete an existing AssignmentItem
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAssignmentItemAsync(AssignmentItem assignmentItem)
        {
            await _assignmentItemRepository.DeleteAsync(assignmentItem);
        }
    }
}
