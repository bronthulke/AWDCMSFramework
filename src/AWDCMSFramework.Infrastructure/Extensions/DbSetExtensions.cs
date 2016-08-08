using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace AWDCMSFramework.Infrastructure.Extensions
{
    public static class DbSetExtensions
    {
        // http://stackoverflow.com/questions/29030472/dbset-doesnt-have-a-find-method-in-ef7/29082410#29082410
        public static TEntity Find<TEntity>(this DbSet<TEntity> set, IServiceProvider serviceProvider, params object[] keyValues) where TEntity : class
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<DbContext>();

                var entityType = context.Model.GetEntityTypes().FirstOrDefault(t => t.GetType() == typeof(TEntity));
                var key = entityType.FindPrimaryKey();

                var entries = context.ChangeTracker.Entries<TEntity>();

                var i = 0;
                foreach (var property in key.Properties)
                {
                    var keyValue = keyValues[i];
                    entries = entries.Where(e => e.Property(property.Name).CurrentValue == keyValue);
                    i++;
                }

                var entry = entries.FirstOrDefault();
                if (entry != null)
                {
                    // Return the local object if it exists.
                    return entry.Entity;
                }

                // TODO: Build the real LINQ Expression
                // set.Where(x => x.Id == keyValues[0]);
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var query = set.Where((Expression<Func<TEntity, bool>>)
                    Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, "Id"),
                            Expression.Constant(keyValues[0])),
                        parameter));

                // Look in the database
                return query.FirstOrDefault();
            }
        }
    }
}
