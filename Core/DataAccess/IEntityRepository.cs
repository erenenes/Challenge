using MyProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyProject.Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity, new() 
    {
        T Get(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter, string[] includes);
        List<T> GetList(Expression<Func<T, bool>> filter, string[] includes);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        int Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
