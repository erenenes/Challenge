using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.Core.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace MyProject.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Where(filter).FirstOrDefault();
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter, string[] includes)
        {
            return GetList(filter, includes).FirstOrDefault();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter, string[] includes)
        {
            using (var context = new TContext())
            {
                var table = context.Set<TEntity>().AsQueryable();
                if (includes != null)
                {
                    foreach (var item in includes)
                    {
                        table = table.Include(item);
                    }
                }
                var list = new List<TEntity>();
                if (filter == null)
                {
                    list = table.ToList();
                }
                else
                {
                    list = table.Where(filter).ToList();
                }

                return list;
            }

        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public int Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity.Id;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
