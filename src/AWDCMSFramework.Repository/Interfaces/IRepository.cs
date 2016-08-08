using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AWDCMSFramework.Domain.Interfaces;

namespace AWDCMSFramework.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> GetListTracked(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity GetSingle(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        IQueryable<TEntity> Find(params Expression<Func<TEntity, object>>[] navigationProperties);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
