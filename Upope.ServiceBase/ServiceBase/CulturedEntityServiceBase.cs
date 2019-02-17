using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Upope.ServiceBase;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.Models;
using Upope.ServiceBase.ServiceBase.Extensions;
using Upope.ServiceBase.ServiceBase.Models;

namespace EczacibasiHealth.Core.ServiceUtilities {
    public interface ICulturedEntityServiceBase<TEntity, TCulturedEntity> : IEntityServiceBase<TEntity>
        where TEntity : class, IEntity, IHasOperator
        where TCulturedEntity : class, IHasParent<TEntity>, IHasCulture, IHasCulturedEntityStatus, ICulturedEntity {
        IQueryable<TCulturedEntity> CulturedEntities { get; }
        TCulturedEntity CulturedEntityGet(int id);
        TEntityParams MapBaseEntity<TEntityParams>(TEntityParams entityParams, int baseEntityId) where TEntityParams : IEntityParams;
        TEntityParams Map<TEntityParams>(int culturedEntityId, Culture culture, bool baseOnCulture = false) where TEntityParams : new();
        IQueryable<TCulturedEntity> UnrelatedEntities(Culture culture, int? id = null);
        RemoveResultStatus RemoveCulturedEntity(IRemoveEntityParams removeEntityParams);
    }

    public abstract class CulturedEntityServiceBase<TEntity, TCulturedEntity> :
        EntityServiceBase<TEntity>, ICulturedEntityServiceBase<TEntity, TCulturedEntity>
        where TEntity : class, IEntity, IHasOperator, IHasCulturedEntities<TCulturedEntity>
        where TCulturedEntity : class, IHasParent<TEntity>, IHasCulture, IHasCulturedEntityStatus, ICulturedEntity, new() {

        private readonly ApplicationDbContext _applicationDbContext;

        protected CulturedEntityServiceBase(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<TCulturedEntity> CulturedEntities {
            get { return _applicationDbContext.Query<TCulturedEntity>().Include(x => x.BaseEntity); }
        }

        public TCulturedEntity CulturedEntityGet(int id) {
            return _applicationDbContext.Find<TCulturedEntity>(id);
        }

        public TEntityParams MapBaseEntity<TEntityParams>(TEntityParams entityParams, int baseEntityId) where TEntityParams : IEntityParams
        {
            var culturedEntity = (IEntity)entityParams;
            var culturedEntityId = culturedEntity != null ? culturedEntity.Id : 0;
            var entity = Entities.First(x => x.Id == baseEntityId);
            Mapper.Map(entity, entityParams);
            ((IEntity)entityParams).Id = culturedEntityId;
            return entityParams;
        }

        public TEntityParams Map<TEntityParams>(int culturedEntityId, Culture culture, bool baseOnCulture = false) where TEntityParams : new()
        {            
            var culturedEntity = CulturedEntities
                .FirstOrDefault(x => x.Id == culturedEntityId && x.Culture == culture);
            var baseEntityId = culturedEntity != null ? culturedEntity.BaseEntityId : 0;
            var entity = Entities.FirstOrDefault(x => x.Id == baseEntityId);

            var entityParams = new TEntityParams();

            if (baseOnCulture) {
                Mapper.Map(entity, entityParams);
                Mapper.Map(culturedEntity ?? new TCulturedEntity(), entityParams);
            } else {
                Mapper.Map(culturedEntity ?? new TCulturedEntity(), entityParams);
                Mapper.Map(entity, entityParams);
            }            

            if (culturedEntity == null && entityParams is IHasCulturedEntityStatus) {
                ((IHasCulturedEntityStatus)entityParams).CulturedEntityStatus = Status.Active;
            }
            else if(culturedEntity != null && entityParams is IHasCulturedEntityStatus) {
                ((IHasCulturedEntityStatus)entityParams).CulturedEntityStatus = culturedEntity.CulturedEntityStatus;
            }

            return entityParams;
        }

        protected virtual void OnCulturedEntitySaveChanged(IEntityParams entityParams, TCulturedEntity culturedEntity) {
        }

        protected virtual void OnCulturedEntitySaveChanges(IEntityParams entityParams
            , TEntity entity, TCulturedEntity culturedEntity) {
        }

        protected override void OnRemove(IRemoveEntityParams removeEntityParams, TEntity entity) {
            var culturedEntity = CulturedEntities.FirstOrDefault(x =>
                x.BaseEntityId == entity.Id && x.Culture == removeEntityParams.Culture);

            if(culturedEntity != null) {
                culturedEntity.CulturedEntityStatus = Status.Removed;
                _applicationDbContext.SaveChanges();

                var hasStatusEntity = entity as IHasStatus;

                if (hasStatusEntity != null)
                {
                    var isAllCulturedEntitiesRemoved = CulturedEntities.All(x =>
                        x.BaseEntityId == entity.Id && x.CulturedEntityStatus == Status.Removed);

                    if (isAllCulturedEntitiesRemoved)
                    {
                        hasStatusEntity.Status = Status.Removed;

                        _applicationDbContext.Update(entity);
                        _applicationDbContext.SaveChanges();
                    }
                }
            }            
        }

        protected override void OnSaveChanged(IEntityParams entityParams, TEntity entity) {
            var baseEntityId = entity.Id;

            if (!(entityParams is IHasCulture)) {
                throw new InvalidOperationException();
            }

            var culturedEntity = CulturedEntities.FirstOrDefault(x =>
                x.CulturedEntityStatus != Status.Removed && x.BaseEntityId == baseEntityId &&
                x.Culture == ((IHasCulture)entityParams).Culture);

            if(culturedEntity == null && entityParams.GetPropertyValue("ContentId") != null) {
                var contentId = (int)entityParams.GetPropertyValue("ContentId");
                culturedEntity = CulturedEntities.FirstOrDefault(x => 
                x.Id == contentId);
            }

            OnCulturedEntitySaveChanges(entityParams, entity, culturedEntity);


            if (culturedEntity == null) {
                var map = Mapper.Map<TCulturedEntity>(entityParams);
                map.BaseEntityId = baseEntityId;

                if (map is OperatorFields && entityParams is IHasOperator) {
                    var operatorFields = map as OperatorFields;
                    var hasOperator = (IHasOperator)entityParams;

                    operatorFields.AssignOperatorFields(OperationType.Create,
                        hasOperator.OperatorType, hasOperator.OperatorId);
                }
                map.CulturedEntityStatus = Status.Active;
                culturedEntity = _applicationDbContext.Add<TCulturedEntity>(map).Entity;
            } else {
                ((IEntity)entityParams).Id = culturedEntity.Id;
                var culturedEntityStatus = culturedEntity.CulturedEntityStatus;
                Mapper.Map(entityParams, culturedEntity,
                    entityParams.GetType(), typeof(TCulturedEntity));

                culturedEntity.CulturedEntityStatus = culturedEntityStatus;
                if (culturedEntity is OperatorFields && entityParams is IHasOperator) {
                    var operatorFields = culturedEntity as OperatorFields;
                    var hasOperator = (IHasOperator)entityParams;

                    operatorFields.AssignOperatorFields(OperationType.Create,
                        hasOperator.OperatorType, hasOperator.OperatorId);
                }
                culturedEntity.BaseEntityId = baseEntityId;
                culturedEntity = _applicationDbContext.Update(culturedEntity).Entity;
            }

            _applicationDbContext.SaveChanges();

            OnCulturedEntitySaveChanged(entityParams, culturedEntity);
            ((IEntity)entityParams).Id = culturedEntity.Id;

            //Delete UnRelated Entities
            var unRelatedEntities = Entities.Where(x => !x.CulturedEntities.Any()).ToList();
            foreach (var unRelatedEntity in unRelatedEntities) {
                Remove(new RemoveEntityParams(unRelatedEntity.Id, unRelatedEntity, culturedEntity.Culture, false, false));
            }
        }

        public IQueryable<TCulturedEntity> UnrelatedEntities(Culture culture, int? baseEntityId = null) {
            return CulturedEntities.Include(x => x.BaseEntity.CulturedEntities).Where(x =>
                x.Culture != culture && x.CulturedEntityStatus != Status.Removed);
        }


        public RemoveResultStatus RemoveCulturedEntity(IRemoveEntityParams removeEntityParams)
        {
            var culturedEntity = CulturedEntityGet(removeEntityParams.Id);

            if (culturedEntity == null)
            {
                return RemoveResultStatus.Success;
            }

            if (culturedEntity is OperatorFields)
            {
                var operatorFields = culturedEntity as OperatorFields;

                operatorFields.AssignOperatorFields(OperationType.Remove,
                    removeEntityParams.OperatorType, removeEntityParams.OperatorId);
            }

            var hasStatusCulturedEntity = culturedEntity as IHasStatus;

            if (hasStatusCulturedEntity != null && removeEntityParams.CheckRelationalEntities)
            {
                foreach (var propertyInfo in culturedEntity.GetType().GetProperties().Where(p =>
                    p.PropertyType.IsGenericType && p.PropertyType
                        .GetGenericTypeDefinition() == typeof(ICollection<>)))
                {
                    var value = propertyInfo.GetValue(culturedEntity, null);
                    var hasStatusRecords = value as IEnumerable<IHasStatus>;

                    if (hasStatusRecords == null)
                    {
                        if (((IEnumerable<IEntity>)value).Any())
                        {
                            return RemoveResultStatus.HasRelatedEntities;
                        }
                    }
                    else
                    {
                        if (hasStatusRecords.Count(x => x.Status != Status.Removed) > 0)
                        {
                            return RemoveResultStatus.HasRelatedEntities;
                        }
                    }
                }
            }
            if (hasStatusCulturedEntity != null && !removeEntityParams.RemovePermanently)
            {
                if (hasStatusCulturedEntity.Status == Status.Removed)
                {
                    return RemoveResultStatus.Success;
                }

                hasStatusCulturedEntity.Status = Status.Removed;

                _applicationDbContext.Update(culturedEntity);
            }
            else
            {
                _applicationDbContext.Remove(culturedEntity);
            }

            _applicationDbContext.SaveChanges();




            var entity = Get(culturedEntity.BaseEntityId);
            culturedEntity.CulturedEntityStatus = Status.Removed;
            _applicationDbContext.SaveChanges();

            var hasStatusEntity = entity as IHasStatus;

            if (hasStatusEntity != null)
            {
                var isAllCulturedEntitiesRemoved = CulturedEntities.All(x =>
                    x.BaseEntityId == entity.Id && x.CulturedEntityStatus == Status.Removed);

                if (isAllCulturedEntitiesRemoved)
                {
                    hasStatusEntity.Status = Status.Removed;

                    _applicationDbContext.Update(entity);
                    _applicationDbContext.SaveChanges();
                }
            }
            OnCulturedEntityRemoved(culturedEntity.Id);

            return RemoveResultStatus.Success;
        }

        protected virtual void OnCulturedEntityRemoved(int contentId) {
            
        }
    }
}