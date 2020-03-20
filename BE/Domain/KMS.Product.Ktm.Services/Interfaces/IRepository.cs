using System.Linq;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Insert(T entity, bool saveChange = true);
        void Update(T entity, bool saveChange = true);
        void Delete(T entity, bool saveChange = true);
    }
}
