using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IKudoTypeService
    {
        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        Task<IEnumerable<KudoType>> GetAllKudoTypesAsync();

        /// <summary>
        /// Get kudo types by id
        /// </summary>
        /// <returns>Returns a kudo type by id</returns>
        Task<KudoType> GetKudoTypeByIdAsync(int kudoTypeId);

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        Task CreateKudoTypeAsync(KudoType kudoType);

        /// <summary>
        /// Update an existing kudo type
        /// </summary>
        /// <returns></returns>
        Task UpdateKudoTypeAsync(KudoType kudoType);

        /// <summary>
        /// Delete an existing kudo type
        /// </summary>
        /// <returns></returns>
        Task DeleteKudoTypeAsync(KudoType kudoType);
    }
}
