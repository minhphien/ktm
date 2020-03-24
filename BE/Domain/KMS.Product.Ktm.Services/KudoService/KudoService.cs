using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.KudoService
{
    public class KudoService : IKudoService
    {
        private readonly IKudoRepository _kudoRepository;

        /// <summary>
        /// Inject Kudo repository
        /// </summary>
        /// <returns></returns>
        public KudoService(IKudoRepository kudoRepository)
        {
            _kudoRepository = kudoRepository ?? throw new ArgumentNullException($"{nameof(kudoRepository)}");
        }

        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<Kudo>> GetAllKudosAsync()
        {
            return await _kudoRepository.GetKudosAsync();
        }

        /// <summary>
        /// Get kudo by id
        /// </summary>
        /// <returns>Returns a kudo by id</returns>
        public async Task<Kudo> GetKudoByIdAsync(int kudoId)
        {
            return await _kudoRepository.GetByIdAsync(kudoId);
        }

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        public async Task CreateKudoAsync(Kudo kudo)
        {
            await _kudoRepository.InsertAsync(kudo);
        }

        /// <summary>
        /// Update an existing kudo
        /// </summary>
        /// <returns></returns>
        public async Task UpdateKudoAsync(Kudo kudo)
        {
            await _kudoRepository.UpdateAsync(kudo);
        }

        /// <summary>
        /// Delete an existing kudo
        /// </summary>
        /// <returns></returns>
        public async Task DeleteKudoAsync(Kudo kudo)
        {
            await _kudoRepository.DeleteAsync(kudo);
        }
    }
}
