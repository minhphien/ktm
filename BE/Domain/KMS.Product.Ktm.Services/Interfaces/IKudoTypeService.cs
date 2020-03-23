using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IKudoTypeService
    {
        IEnumerable<KudoType> GetAllKudoTypes();
        KudoType GetKudoTypeById(int kudoTypeId);
        void CreateKudoType(KudoType kudoType);
        void UpdateKudoType(KudoType kudoType);
        void DeleteKudoType(KudoType kudoType);
    }
}
