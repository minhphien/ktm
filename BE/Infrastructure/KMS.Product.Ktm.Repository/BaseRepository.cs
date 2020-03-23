using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly KtmDbContext context;
        private readonly DbSet<T> entities;

        public BaseRepository(KtmDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }
        
        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task InsertAsync(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;

            entities.Add(entity);

            if (saveChange)
                await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.Modified = DateTime.Now;
            entities.Update(entity);
            if (saveChange)
                await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            if (saveChange)
                await context.SaveChangesAsync();
        }
    }
}
