using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AWDCMSFramework.Domain;

namespace AWDCMSFramework.Repository.Interfaces
{
    public interface ISitePageRepository
    {
        IEnumerable<SitePage> GetAll(params Expression<Func<SitePage, object>>[] navigationProperties);
        IEnumerable<SitePage> GetList(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties);
        IEnumerable<SitePage> GetListTracked(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties);
        SitePage GetSingle(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties);

        IQueryable<SitePage> Find(params Expression<Func<SitePage, object>>[] navigationProperties);
        IQueryable<SitePage> Find(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties);

        IList<SitePage> GetListRecursive(Expression<Func<SitePage, bool>> where, params Expression<Func<SitePage, object>>[] navigationProperties);
        SitePage GetSingleForAlias(string SitePageAlias, bool IsLiveOnly);

        void Insert(SitePage entity);
        void Update(SitePage entity);
        void ClearLayouts(int sitePageId);
        void Delete(int id);
    }
}
