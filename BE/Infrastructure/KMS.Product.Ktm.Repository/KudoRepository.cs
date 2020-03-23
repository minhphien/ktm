using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class KudoRepository : BaseRepository<Kudo>, IKudoRepository
    {
        public KudoRepository(KtmDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<Kudo>> GetKudosAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }
    }
}
