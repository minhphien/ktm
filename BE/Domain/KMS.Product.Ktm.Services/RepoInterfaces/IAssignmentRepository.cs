using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface IAssignmentRepository : IBaseRepository<Assignment>
    {
        /// <summary>
        /// Get all Assignments
        /// </summary>
        /// <returns>Returns a collection of all Assignments</returns>
        Task<IEnumerable<Assignment>> GetAssignmentsAsync();
    }
}