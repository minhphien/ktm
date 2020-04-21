using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.AssignmentItemService
{
    public interface IAssignmentItemService
    {
        /// <summary>
        /// Get all assignmentItems
        /// </summary>
        /// <returns>Returns a collection of all assignmentItems</returns>
        Task<IEnumerable<AssignmentItem>> GetAllAssignmentItemsAsync();

        /// <summary>
        /// Get assignmentItems by id
        /// </summary>
        /// <returns>Returns a assignmentItem by id</returns>
        Task<AssignmentItem> GetAssignmentItemByIdAsync(int assignmentItemId);

        /// <summary>
        /// Create a new assignmentItem
        /// </summary>
        /// <returns></returns>
        Task CreateAssignmentItemAsync(AssignmentItem assignmentItem);

        /// <summary>
        /// Update an existing assignmentItem
        /// </summary>
        /// <returns></returns>
        Task UpdateAssignmentItemAsync(AssignmentItem assignmentItem);

        /// <summary>
        /// Delete an existing assignmentItem
        /// </summary>
        /// <returns></returns>
        Task DeleteAssignmentItemAsync(AssignmentItem assignmentItem);
    }
}

