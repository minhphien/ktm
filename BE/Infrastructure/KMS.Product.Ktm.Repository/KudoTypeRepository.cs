using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class KudoTypeRepository : BaseRepository<KudoType>, IKudoTypeRepository
    {
        public KudoTypeRepository(KtmDbContext context, ILogger<KudoType> logger) : base(context, logger)
        {
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        public async Task<IEnumerable<KudoType>> GetKudoTypesAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
