using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AWDCMSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using AWDCMSFramework.Domain.Interfaces;
using System.Security.Claims;

namespace AWDCMSFramework.Repository
{
    public class AWDCMSContext : IdentityDbContext<ApplicationUser>
    {
        public AWDCMSContext(DbContextOptions<AWDCMSContext> options) : base(options)
        {
            //this.Configuration.LazyLoadingEnabled = false; // B. Squire: need to double check if I need this, or if I've set up my virtuals correctly already
        }


        public DbSet<SitePage> SitePages { get; set; }
        public DbSet<SitePageLayoutArea> SitePageLayoutAreas { get; set; }
        public DbSet<SitePageTemplate> SiteTemplates { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsArticleCategory> NewsArticleCategories { get; set; }
        public DbSet<CarouselItem> CarouselItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SitePage>()
                //.HasKey(u => u.ID)
                .HasMany(u => u.LayoutAreas);     // B. Squire - .Net Core upgrade change
                //.WithRequired()
                //.HasForeignKey(h => h.SitePageId);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(
                    e => e.Entity is ILogInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var userName = ClaimsPrincipal.Current.Identity.Name;

                if (entry.State == EntityState.Added)
                {
                    ((ILogInfo)entry.Entity).CreateUser = userName;
                    ((ILogInfo)entry.Entity).CreateDate = DateTime.Now;
                }

                ((ILogInfo)entry.Entity).UpdateUser = userName;
                ((ILogInfo)entry.Entity).UpdateDate = DateTime.Now;
            }

            return base.SaveChanges();
        }

    }
}
