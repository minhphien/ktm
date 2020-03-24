using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{    
    public interface IKudoTypeRepository : IBaseRepository<KudoType>
    {
        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        Task<IEnumerable<KudoType>> GetKudoTypesAsync();
    }
}
