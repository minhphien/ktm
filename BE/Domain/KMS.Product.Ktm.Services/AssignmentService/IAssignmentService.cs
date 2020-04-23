using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.AssignmentService
{
    public interface IAssignmentService
    {
        /// <summary>
        /// Get all assignments
        /// </summary>
        /// <returns>Returns a collection of all assignments</returns>
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();

        /// <summary>
        /// Get assignments by id
        /// </summary>
        /// <returns>Returns a assignment by id</returns>
        Task<Assignment> GetAssignmentByIdAsync(int assignmentId);

        /// <summary>
        /// Create a new assignment
        /// </summary>
        /// <returns></returns>
        Task CreateAssignmentAsync(Assignment assignment);

        /// <summary>
        /// Update an existing assignment
        /// </summary>
        /// <returns></returns>
        Task UpdateAssignmentAsync(Assignment assignment);

        /// <summary>
        /// Delete an existing assignment
        /// </summary>
        /// <returns></returns>
        Task DeleteAssignmentAsync(Assignment assignment);
    }
}

