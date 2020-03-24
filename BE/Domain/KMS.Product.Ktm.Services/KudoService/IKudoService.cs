using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.KudoService
{
    public interface IKudoService
    {
        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        Task<IEnumerable<Kudo>> GetAllKudosAsync();

        /// <summary>
        /// Get kudo by id
        /// </summary>
        /// <returns>Returns a kudo by id</returns>
        Task<Kudo> GetKudoByIdAsync(int kudoId);

        /// <summary>
        /// Create a new kudo
        /// </summary>
        /// <returns></returns>
        Task CreateKudoAsync(Kudo kudo);

        /// <summary>
        /// Update an existing kudo
        /// </summary>
        /// <returns></returns>
        Task UpdateKudoAsync(Kudo kudo);

        /// <summary>
        /// Delete an existing kudo
        /// </summary>
        /// <returns></returns>
        Task DeleteKudoAsync(Kudo kudo);
    }
}
