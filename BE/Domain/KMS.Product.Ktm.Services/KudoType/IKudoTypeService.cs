using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IKudoTypeService
    {
        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudo types</returns>
        IEnumerable<KudoType> GetAllKudoTypes();

        /// <summary>
        /// Get kudo types by id
        /// </summary>
        /// <returns>Returns a kudo type by id</returns>
        KudoType GetKudoTypeById(int kudoTypeId);

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        void CreateKudoType(KudoType kudoType);

        /// <summary>
        /// Update an existing kudo type
        /// </summary>
        /// <returns></returns>
        void UpdateKudoType(KudoType kudoType);

        /// <summary>
        /// Delete an existing kudo type
        /// </summary>
        /// <returns></returns>
        void DeleteKudoType(KudoType kudoType);
    }
}
