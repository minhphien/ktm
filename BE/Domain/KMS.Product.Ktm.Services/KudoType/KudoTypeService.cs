using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Services.Implement
{
    public class KudoTypeService : IKudoTypeService
    {
        private readonly IRepository<KudoType> _kudoTypeRepository;

        /// <summary>
        /// Inject KudoType repository
        /// </summary>
        /// <returns></returns>
        public KudoTypeService(IRepository<KudoType> kudoTypeRepository)
        {
            _kudoTypeRepository = kudoTypeRepository ?? throw new ArgumentNullException($"{nameof(kudoTypeRepository)}");
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        public IEnumerable<KudoType> GetAllKudoTypes()
        {
            return _kudoTypeRepository.GetAll();
        }

        /// <summary>
        /// Get kudo types by id
        /// </summary>
        /// <returns>Returns a kudo type by id</returns>
        public KudoType GetKudoTypeById(int kudoTypeId)
        {
            return _kudoTypeRepository.Get(kudoTypeId);
        }

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        public void CreateKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Insert(kudoType);
        }

        /// <summary>
        /// Update an existing kudo type
        /// </summary>
        /// <returns></returns>
        public void UpdateKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Update(kudoType);
        }

        /// <summary>
        /// Delete an existing kudo type
        /// </summary>
        /// <returns></returns>
        public void DeleteKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Delete(kudoType);
        }
    }
}
