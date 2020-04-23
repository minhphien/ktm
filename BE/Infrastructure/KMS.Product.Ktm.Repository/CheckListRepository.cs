using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class CheckListRepository : BaseRepository<CheckList>, ICheckListRepository
    {
        public CheckListRepository(KtmDbContext context, ILogger<CheckList> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all CheckLists
        /// </summary>
        /// <returns>Returns a collection of all CheckLists</returns>
        public async Task<IEnumerable<CheckList>> GetCheckListsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
