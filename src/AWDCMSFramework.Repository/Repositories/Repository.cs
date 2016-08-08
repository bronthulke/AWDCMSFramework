using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWDCMSFramework.Infrastructure.Extensions;
using AWDCMSFramework.Repository.Interfaces;

namespace AWDCMSFramework.Repository
{
   
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IServiceProvider _serviceProvider;

        public Repository(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
          //  _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            _dbSet = context.Set<TEntity>();

            _serviceProvider = serviceProvider;
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<TEntity>();
            return list;
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<TEntity>();
            return list;
        }

        public virtual IEnumerable<TEntity> GetListTracked(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery
                .Where(where)
                .ToList<TEntity>();
            return list;
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            item = dbQuery
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public virtual IQueryable<TEntity> Find(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            var query = dbQuery
                .AsNoTracking();

            return query.AsQueryable();
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _context.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            var query = dbQuery
                .AsNoTracking()
                .Where(where);

            return query.AsQueryable();
        }


        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(_serviceProvider, id);
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }
}
