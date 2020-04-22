using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class assignmentItemRepository : BaseRepository<AssignmentItem>, IAssignmentItemRepository
    {
        public assignmentItemRepository(KtmDbContext context, ILogger<AssignmentItem> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all assignmentItems
        /// </summary>
        /// <returns>Returns a collection of all assignmentItems</returns>
        public async Task<IEnumerable<AssignmentItem>> GetAssignmentItemsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
