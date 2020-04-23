using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface IAssignmentItemRepository : IBaseRepository<AssignmentItem>
    {
        /// <summary>
        /// Get all AssignmentItems
        /// </summary>
        /// <returns>Returns a collection of all AssignmentItems</returns>
        Task<IEnumerable<AssignmentItem>> GetAssignmentItemsAsync();
    }
}