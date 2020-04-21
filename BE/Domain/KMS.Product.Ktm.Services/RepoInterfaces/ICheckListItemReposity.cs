using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface ICheckListItemRepository : IBaseRepository<CheckListItem>
    {
        /// <summary>
        /// Get all CheckListItems
        /// </summary>
        /// <returns>Returns a collection of all CheckListItems</returns>
        Task<IEnumerable<CheckListItem>> GetCheckListItemsAsync();
    }
}