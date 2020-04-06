using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    /// <summary>
    /// An interface of base repository using asynchronous operations for other inteface repositories to inherit
    /// </summary>
    /// <typeparam name="T">BaseEntity</typeparam>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An entity by id</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChange"></param>
        /// <returns></returns>
        Task InsertAsync(T entity, bool saveChange = true);

        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChange"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity, bool saveChange = true);

        /// <summary>
        /// Delete an existing entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChange"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool saveChange = true);
    }
}
