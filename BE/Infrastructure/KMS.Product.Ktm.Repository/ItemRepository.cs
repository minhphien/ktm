using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(KtmDbContext context, ILogger<Item> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>Returns a collection of all items</returns>
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
