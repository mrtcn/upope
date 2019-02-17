using System;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.ServiceBase.ServiceBase.Extensions
{
    public static class ServiceExtensions
    {
        public static T AssignOperatorFields<T>(this T entity, OperationType operationType,
            OperatorType operatorType, int operatorId) where T : OperatorFields
        {
            switch (operationType)
            {
                case OperationType.Create:
                    entity.CreatedUserType = operatorType;
                    entity.CreatedDate = DateTime.Now;
                    entity.CreatedBy = operatorId;
                    break;
                case OperationType.Update:
                case OperationType.Remove:
                    entity.LastModifiedUserType = operatorType;
                    entity.LastModifiedDate = DateTime.Now;
                    entity.LastModifiedBy = operatorId;
                    break;
            }

            return entity;
        }

        public static T AssignOperatorFields<T>(this T entity, OperationType operationType,
            IHasOperator hasOperator) where T : OperatorFields
        {
            return AssignOperatorFields(entity, operationType,
                hasOperator.OperatorType, hasOperator.OperatorId);
        }

        public static T AssignOperatorFields<T>(this T destination, IHasOperator source) where T : IHasOperator
        {
            destination.OperatorType = source.OperatorType;
            destination.OperatorId = source.OperatorId;

            return destination;
        }
    }
}
