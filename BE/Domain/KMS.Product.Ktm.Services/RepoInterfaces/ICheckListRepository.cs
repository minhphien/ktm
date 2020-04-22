using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface ICheckListRepository : IBaseRepository<CheckList>
    {
        /// <summary>
        /// Get all CheckLists
        /// </summary>
        /// <returns>Returns a collection of all CheckLists</returns>
        Task<IEnumerable<CheckList>> GetCheckListsAsync();
    }
}