using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Upope.ServiceBase.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Upope.ServiceBase.DataManagement {
    public interface IRepository<TEntity> : IQueryable where TEntity : class, IEntity {
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity Create(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null);
        TEntity First(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        IObjectSet<TEntity> GetObjectSet { get; }
        void SaveChanges();
        Task SaveChangesAsync();
        TEntity Single(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate = null);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate = null);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> Table { get; }
        TEntity Update(TEntity entity);
    }
}