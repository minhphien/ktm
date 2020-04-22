using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>Returns a collection of all items</returns>
        Task<IEnumerable<Item>> GetItemsAsync();
    }
}