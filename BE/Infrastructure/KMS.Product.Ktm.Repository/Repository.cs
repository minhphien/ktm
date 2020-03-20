using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using KMS.Product.Ktm.Services.Interfaces;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Repository
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly KtmDbContext context;
        private readonly DbSet<T> entities;

        public Repository(KtmDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T Get(int id)
        {
            return entities.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return entities.Where(filter);
        }

        public void Insert(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;

            entities.Add(entity);

            if (saveChange)
                context.SaveChanges();

        }

        public void Update(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.Modified = DateTime.Now;
            if (saveChange)
                context.SaveChanges();
        }

        public void Delete(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            if (saveChange)
                context.SaveChanges();
        }
    }
}
