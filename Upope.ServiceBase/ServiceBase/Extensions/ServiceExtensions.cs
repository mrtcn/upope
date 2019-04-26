using System;
using Upope.ServiceBase.Enums;
using Upope.ServiceBase.Interfaces;
using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.ServiceBase.ServiceBase.Extensions
{
    public static class ServiceExtensions
    {
        public static T AssignOperatorFields<T>(this T entity, OperationType operationType) where T : IDateOperationFields
        {
            switch (operationType)
            {
                case OperationType.Create:
                    entity.CreatedDate = DateTime.Now;
                    break;
                case OperationType.Update:
                case OperationType.Remove:
                    entity.LastModifiedDate = DateTime.Now;
                    break;
            }

            return entity;
        }

        public static T AssignOperatorFields<T>(this T entity) where T : IDateOperationFields
        {
            return AssignOperatorFields(entity, OperationType.Create);
        }
    }
}
