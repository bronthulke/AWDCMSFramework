using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWDCMSFramework.Domain;
using AWDCMSFramework.Repository.Interfaces;
using AWDCMSFramework.Infrastructure.Extensions;

namespace AWDCMSFramework.Repository.Repositories
{
    public class SitePageRepository : ISitePageRepository
    {
        private readonly AWDCMSContext _context;
        private readonly IServiceProvider _serviceProvider;

        public SitePageRepository(AWDCMSContext context, IServiceProvider serviceProvider)
        {
            _context = context;

            _serviceProvider = serviceProvider;
        }

        public virtual IEnumerable<SitePage> GetAll(params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            List<SitePage> list;
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            list = dbQuery
                .Include(s => s.Template)
                .AsNoTracking()
                .ToList<SitePage>();
            return list;
        }

        public virtual IEnumerable<SitePage> GetList(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            List<SitePage> list;
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Include(s => s.Template)
                .Where(where)
                .ToList<SitePage>();
            return list;
        }

        public virtual IEnumerable<SitePage> GetListTracked(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            List<SitePage> list;
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            list = dbQuery
                .Include(s => s.Template)
                .Where(where)
                .ToList<SitePage>();
            return list;
        }

        public virtual SitePage GetSingle(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            SitePage item = null;
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            item = dbQuery
                .Include(s => s.Template)
                .AsNoTracking()
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public virtual IQueryable<SitePage> Find(params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            var query = dbQuery
                .AsNoTracking()
                .Include(s => s.Template);

            return query.AsQueryable();
        }

        public virtual IQueryable<SitePage> Find(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            var query = dbQuery
                .AsNoTracking()
                .Include(s => s.Template)
                .Where(where);

            return query.AsQueryable();
        }

        /// <summary>
        /// This function behaves much like GetList, but it doesn't implicitely close off the DB Context, so we can get our recursive site structure
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<SitePage> GetListRecursive(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties)
        {
            List<SitePage> list;
           // _context.Configuration.LazyLoadingEnabled = true; // B. Squire - TO DO: Revisit this now LL is not available in EFCore
            IQueryable<SitePage> dbQuery = _context.Set<SitePage>();

            //Apply eager loading
            foreach (Expression<Func<SitePage, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<SitePage, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Include(s => s.Template)
                .Where(where)
                .ToList<SitePage>();
            return list;
        }

        public virtual SitePage GetSingleForAlias(string SitePageAlias, bool IsLiveOnly)
        {
            //_context.Configuration.LazyLoadingEnabled = true; // B. Squire - TO DO: Revisit this now LL is not available in EFCore
            return _context.SitePages
                .Include(s => s.Parent)
                .Include(s => s.Children)
                .Include(s => s.Template)
                .Include(s => s.LayoutAreas)
                .Where(s => s.Alias.ToLower() == SitePageAlias.ToLower() && (!IsLiveOnly || s.IsLive))
                .FirstOrDefault();
        }

        public void Insert(SitePage entity)
        {
            // flag all layouts for insert, too
            if (entity.LayoutAreas != null)
            {
                foreach (var layout in entity.LayoutAreas)
                {
                    _context.Entry(layout).State = EntityState.Added;
                }
            }

            _context.SitePages.Add(entity);
            _context.SaveChanges();
        }

        public void Update(SitePage entity)
        {
            // TODO: implement this
            throw new NotImplementedException("B. Squire - TO DO: implement this, need to work out how to replace the use of DbEntityEntry<SitePage>.CurrentValues");

//            var entityInDb = _context.SitePages.Include(s => s.LayoutAreas)
//                .Single(s => s.Id == entity.Id);

//            // Update scalar/complex properties of parent
//            _context.Entry(entityInDb).CurrentValues.SetValues(entity);

//            var layoutAreasInDb = entityInDb.LayoutAreas.ToList();
//            foreach (var layoutAreaInDb in layoutAreasInDb)
//            {
//                // Is the area still there?
//                var layoutArea = entity.LayoutAreas
//                    .SingleOrDefault(i => i.Id == layoutAreaInDb.Id);

//                if (layoutArea != null)
//                    // Yes: Update scalar/complex properties of child
//                    _context.Entry(layoutAreaInDb).CurrentValues.SetValues(layoutArea);
//                else
//                    // No: Delete it
//                    _context.SitePageLayoutAreas.Remove(layoutAreaInDb);
//            }

//            foreach (var layoutArea in entity.LayoutAreas)
//            {
//                // Is the child NOT in DB?
//                if (!layoutAreasInDb.Any(i => i.Id == layoutArea.Id))
//                    // Yes: Add it as a new child
//                    entityInDb.LayoutAreas.Add(layoutArea);
//            }

////            _context.SitePages.Attach(entity);
////            _context.Entry(entity).State = EntityState.Modified;
//            _context.SaveChanges();
        }

        public void ClearLayouts(int sitePageId)
        {
            // flag all layouts for delete
            var page = _context.SitePages.SingleOrDefault(p => p.Id == sitePageId);
            page.LayoutAreas.Clear();
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = _context.SitePages.Find(_serviceProvider, id);
            _context.SitePages.Remove(entityToDelete);
            _context.SaveChanges();
        }
    }
}