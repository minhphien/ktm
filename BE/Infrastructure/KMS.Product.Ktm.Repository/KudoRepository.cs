using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class KudoRepository : BaseRepository<Kudo>, IKudoRepository
    {
        private readonly KtmDbContext context;
        private readonly DbSet<Kudo> kudo;

        public KudoRepository(KtmDbContext context) : base(context)
        {
            this.context = context;
            kudo = context.Set<Kudo>();
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<Kudo>> GetKudosAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }

        /// <summary>
        /// insert mutiply kudo
        /// </summary>
        /// <param name="kudos"></param>
        /// <returns></returns>
        public async Task InsertKudos(IEnumerable<Kudo> kudos)
        {
            kudo.AddRange(kudos);
            await context.SaveChangesAsync();
        }
    }
}
