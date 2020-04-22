using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.AssignmentService
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        /// <summary>
        /// Inject Assignment repository
        /// </summary>
        /// <returns></returns>
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository ?? throw new ArgumentNullException($"{nameof(assignmentRepository)}");
        }

        /// <summary>
        /// Get all Assignments
        /// </summary>
        /// <returns>Returns a collection of all Assignments</returns>
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await _assignmentRepository.GetAssignmentsAsync();
        }

        /// <summary>
        /// Get Assignments by id
        /// </summary>
        /// <returns>Returns a Assignment by id</returns>
        public async Task<Assignment> GetAssignmentByIdAsync(int assignmentId)
        {
            return await _assignmentRepository.GetByIdAsync(assignmentId);
        }

        /// <summary>
        /// Create a new Assignment
        /// </summary>
        /// <returns></returns>
        public async Task CreateAssignmentAsync(Assignment assignment)
        {
            await _assignmentRepository.InsertAsync(assignment);
        }

        /// <summary>
        /// Update an existing Assignment
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAssignmentAsync(Assignment assignment)
        {
            await _assignmentRepository.UpdateAsync(assignment);
        }

        /// <summary>
        /// Delete an existing Assignment
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAssignmentAsync(Assignment assignment)
        {
            await _assignmentRepository.DeleteAsync(assignment);
        }
    }
}
