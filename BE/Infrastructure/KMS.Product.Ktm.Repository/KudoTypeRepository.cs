using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class KudoTypeRepository : BaseRepository<KudoType>, IKudoTypeRepository
    {
        public KudoTypeRepository(KtmDbContext context) : base(context)
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
