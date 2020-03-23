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

        public KudoTypeService(IRepository<KudoType> kudoTypeRepository)
        {
            _kudoTypeRepository = kudoTypeRepository;
        }

        public IEnumerable<KudoType> GetAllKudoTypes()
        {
            return _kudoTypeRepository.GetAll();
        }

        public KudoType GetKudoTypeById(int kudoTypeId)
        {
            return _kudoTypeRepository.Get(kudoTypeId);
        }

        public void CreateKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Insert(kudoType);
        }

        public void UpdateKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Update(kudoType);
        }

        public void DeleteKudoType(KudoType kudoType)
        {
            _kudoTypeRepository.Delete(kudoType);
        }
    }
}
