using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(KtmDbContext context, ILogger<Assignment> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all Assignments
        /// </summary>
        /// <returns>Returns a collection of all Assignments</returns>
        public async Task<IEnumerable<Assignment>> GetAssignmentsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
