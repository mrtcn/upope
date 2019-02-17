using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Upope.ServiceBase.Interfaces;

namespace Upope.ServiceBase.DataManagement {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity {
        private readonly IObjectSet<TEntity> _objectSet;
        private readonly IObjectSetFactory _objectSetFactory;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IObjectSetFactory objectSetFactory, IUnitOfWork unitOfWork) {
            _objectSet = objectSetFactory.CreateObjectSet<TEntity>();
            _objectSetFactory = objectSetFactory;
            _unitOfWork = unitOfWork;
        }

        private IQueryable<TEntity> AsQueryable {
            get { return _objectSet; }
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.Count(predicate ?? (x => true));
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null) {
            return await AsQueryable.CountAsync(predicate ?? (x => true));
        }

        public virtual TEntity Create(TEntity entity) {
            _objectSet.AddObject(entity);

            return entity;
        }

        public virtual void Delete(TEntity entity) {
            _objectSet.DeleteObject(entity);
            _objectSetFactory.ChangeObjectState(entity, EntityState.Deleted);
        }

        public Type ElementType {
            get { return _objectSet.ElementType; }
        }

        public Expression Expression {
            get { return _objectSet.Expression; }
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.Where(predicate ?? (x => true));
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.First(predicate ?? (x => true));
        }

        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null) {
            return await AsQueryable.FirstAsync(predicate ?? (x => true));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.FirstOrDefault(predicate ?? (x => true));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null) {
            return await AsQueryable.FirstOrDefaultAsync(predicate ?? (x => true));
        }

        public virtual TEntity Get(int id) {
            return id == 0 ? null : SingleOrDefault(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(int id) {
            return await SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerator GetEnumerator() {
            return _objectSet.GetEnumerator();
        }

        public IObjectSet<TEntity> GetObjectSet {
            get { return _objectSet; }
        }

        public IQueryProvider Provider {
            get { return _objectSet.Provider; }
        }

        public void SaveChanges() {
            _unitOfWork.Commit();
        }

        public async Task SaveChangesAsync() {
            await _unitOfWork.CommitAsync();
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.Single(predicate ?? (x => true));
        }

        public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate = null) {
            return await AsQueryable.SingleAsync(predicate ?? (x => true));
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate = null) {
            return AsQueryable.SingleOrDefault(predicate ?? (x => true));
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null) {
            return await AsQueryable.SingleOrDefaultAsync(predicate ?? (x => true));
        }

        public virtual TEntity Update(TEntity entity) {
            _objectSet.Attach(entity);
            _objectSetFactory.ChangeObjectState(entity, EntityState.Modified);

            return entity;
        }

        public virtual IQueryable<TEntity> Table {
            get { return AsQueryable; }
        }
    }
}