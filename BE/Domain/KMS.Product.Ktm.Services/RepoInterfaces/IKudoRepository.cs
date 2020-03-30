using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{    
    public interface IKudoRepository : IBaseRepository<Kudo>
    {
        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        Task<IEnumerable<Kudo>> GetKudosAsync();

        /// <summary>
        /// insert mutiply kudo
        /// </summary>
        /// <param name="kudos"></param>
        /// <returns></returns>
        Task InsertKudos(IEnumerable<Kudo> kudo);
    }
}
