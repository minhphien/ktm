using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity, bool saveChange = true);
        Task UpdateAsync(T entity, bool saveChange = true);
        Task DeleteAsync(T entity, bool saveChange = true);
    }
}
