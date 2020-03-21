using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using TraPa.Entities.Impl.Classes;

namespace TraPa.DataAccess.EFCore.Public.Interfaces
{
    public interface IDatabaseContext : IDisposable, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbContextPoolable
    {
        DbSet<Traveler> Travelers { get; set; }
        DbSet<TravelDate> TravelDates { get; set; }
        DbSet<TravelerTravelDateReference> TravelerTravelDateReferences { get; set; }
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        EntityEntry Entry(object entity);
        EntityEntry Add(object entity);
        EntityEntry Attach(object entity);
        EntityEntry Update(object entity);
        EntityEntry Remove(object entity);
        void AddRange(params object[] entities);
        Task AddRangeAsync(params object[] entities);
        void AttachRange(params object[] entities);
        void UpdateRange(params object[] entities);
        void RemoveRange(params object[] entities);
        void AddRange(IEnumerable<object> entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken);
        void AttachRange(IEnumerable<object> entities);
        void UpdateRange(IEnumerable<object> entities);
        void RemoveRange(IEnumerable<object> entities);
        object Find(Type entityType, params object[] keyValues);
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
    }
}