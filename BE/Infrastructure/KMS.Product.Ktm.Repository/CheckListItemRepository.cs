using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class CheckListItemRepository : BaseRepository<CheckListItem>, ICheckListItemRepository
    {
        public CheckListItemRepository(KtmDbContext context, ILogger<CheckListItem> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all CheckListItems
        /// </summary>
        /// <returns>Returns a collection of all CheckListItems</returns>
        public async Task<IEnumerable<CheckListItem>> GetCheckListItemsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
