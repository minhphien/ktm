using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Services.KudoTypeService
{
    public class KudoTypeService : IKudoTypeService
    {
        private readonly IKudoTypeRepository _kudoTypeRepository;

        /// <summary>
        /// Inject KudoType repository
        /// </summary>
        /// <returns></returns>
        public KudoTypeService(IKudoTypeRepository kudoTypeRepository)
        {
            _kudoTypeRepository = kudoTypeRepository ?? throw new ArgumentNullException($"{nameof(kudoTypeRepository)}");
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        public async Task<IEnumerable<KudoType>> GetAllKudoTypesAsync()
        {
            return await _kudoTypeRepository.GetKudoTypesAsync();
        }

        /// <summary>
        /// Get kudo types by id
        /// </summary>
        /// <returns>Returns a kudo type by id</returns>
        public async Task<KudoType> GetKudoTypeByIdAsync(int kudoTypeId)
        {
            return await _kudoTypeRepository.GetByIdAsync(kudoTypeId);
        }

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        public async Task CreateKudoTypeAsync(KudoType kudoType)
        {
            await _kudoTypeRepository.InsertAsync(kudoType);
        }

        /// <summary>
        /// Update an existing kudo type
        /// </summary>
        /// <returns></returns>
        public async Task UpdateKudoTypeAsync(KudoType kudoType)
        {
            await _kudoTypeRepository.UpdateAsync(kudoType);
        }

        /// <summary>
        /// Delete an existing kudo type
        /// </summary>
        /// <returns></returns>
        public async Task DeleteKudoTypeAsync(KudoType kudoType)
        {
            await _kudoTypeRepository.DeleteAsync(kudoType);
        }
    }
}
