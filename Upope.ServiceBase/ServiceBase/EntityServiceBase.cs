using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.ServiceBase.Extensions;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.ServiceBase {
    public interface IEntityServiceBase<TEntity> : IDependency where TEntity : class, IEntity {
        IQueryable<TEntity> Entities { get; }
        TEntity Get(int id);        
        TEntity CreateOrUpdate(IEntityParams entityParams);
        RemoveResultStatus Remove(IRemoveEntityParams removeEntityParams);
    }

    public abstract class EntityServiceBase<TEntity> : IEntityServiceBase<TEntity> where TEntity : class, IEntity {
        private readonly DbContext _applicationDbContext;
        private readonly IMapper _mapper;
        protected EntityServiceBase(DbContext applicationDbContext, IMapper mapper) {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        private TEntity CreateEntity(IEntityParams entityParams) {
            var entity = _mapper.Map<TEntity>(entityParams);

            if (entity is OperatorFields && entityParams is IHasOperator) {
                var operatorFields = entity as OperatorFields;
                var hasOperator = (IHasOperator)entityParams;

                operatorFields.AssignOperatorFields(OperationType.Create,
                    hasOperator.OperatorType, hasOperator.OperatorId);
            }

            OnSaveChanges(entityParams, entity);

            if(entity is IHasStatus && entityParams is IHasStatus)
            {
                var statusParams = entityParams as IHasStatus;
                statusParams.Status = Status.Active;
            }
            
            _mapper.Map(entityParams, entity, entityParams.GetType(), typeof(TEntity));

            _applicationDbContext.Add(entity);
            _applicationDbContext.SaveChanges();

            entityParams.SetPropertyValue("Id", entity.Id);
            
            OnSaveChanged(entityParams, entity);

            return entity;
        }

        public virtual TEntity CreateOrUpdate(IEntityParams entityParams) {
            return ((IEntity)entityParams).Id == default(int) ? CreateEntity(entityParams) : UpdateEntity(entityParams);
        }

        public virtual TEntity Get(int id) {
            return _applicationDbContext.Find<TEntity>(id);
        }

        protected virtual void OnRemove(IRemoveEntityParams removeEntityParams, TEntity entity) {
        }

        protected virtual void OnRemoved(IRemoveEntityParams removeEntityParams, TEntity entity) {
        }

        protected virtual void OnSaveChanged(IEntityParams entityParams, TEntity entity) {

        }

        protected virtual void OnSaveChanges(IEntityParams entityParams, TEntity entity) {            
        }

        public RemoveResultStatus Remove(IRemoveEntityParams removeEntityParams) {
            var entity = Get(removeEntityParams.Id);

            if (entity == null) {
                return RemoveResultStatus.Success;
            }

            if (entity is OperatorFields) {
                var operatorFields = entity as OperatorFields;

                operatorFields.AssignOperatorFields(OperationType.Remove,
                    removeEntityParams.OperatorType, removeEntityParams.OperatorId);
            }

            OnRemove(removeEntityParams, entity);

            var hasStatusEntity = entity as IHasStatus;

            if (hasStatusEntity != null && removeEntityParams.CheckRelationalEntities) {
                foreach (var propertyInfo in entity.GetType().GetProperties().Where(p =>
                    p.PropertyType.IsGenericType && p.PropertyType
                        .GetGenericTypeDefinition() == typeof(ICollection<>))) {
                    var value = propertyInfo.GetValue(entity, null);
                    var hasStatusRecords = value as IEnumerable<IHasStatus>;

                    if (hasStatusRecords == null) {
                        if (((IEnumerable<IEntity>)value).Any()) {
                            return RemoveResultStatus.HasRelatedEntities;
                        }
                    } else {
                        if (hasStatusRecords.Count(x => x.Status != Status.Removed) > 0) {
                            return RemoveResultStatus.HasRelatedEntities;
                        }
                    }
                }
            }
            if (hasStatusEntity != null && !removeEntityParams.RemovePermanently) {
                if (hasStatusEntity.Status == Status.Removed) {
                    return RemoveResultStatus.Success;
                }

                hasStatusEntity.Status = Status.Removed;

                _applicationDbContext.Update(entity);
            } else {
                _applicationDbContext.Remove(entity);
            }

            _applicationDbContext.SaveChanges();

            OnRemoved(removeEntityParams, entity);

            return RemoveResultStatus.Success;
        }

        private TEntity UpdateEntity(IEntityParams entityParams) {
            var entity = Get(((IEntity)entityParams).Id);

            OnSaveChanges(entityParams, entity);

            if (entity is OperatorFields && entityParams is IHasOperator) {
                var operatorFields = entity as OperatorFields;
                var hasOperator = (IHasOperator)entityParams;

                operatorFields.AssignOperatorFields(OperationType.Update,
                    hasOperator.OperatorType, hasOperator.OperatorId);
            }

            _mapper.Map(entityParams, entity,
                entityParams.GetType(), typeof(TEntity));

            _applicationDbContext.Update(entity);
            _applicationDbContext.SaveChanges();

            OnSaveChanged(entityParams, entity);

            return entity;
        }

        public virtual IQueryable<TEntity> Entities {
            get { return _applicationDbContext.Set<TEntity>(); }
        }
    }
}