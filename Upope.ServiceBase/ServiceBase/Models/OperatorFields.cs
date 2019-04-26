using System;

namespace Upope.ServiceBase.ServiceBase.Models
{
    [Serializable]
    public abstract class OperatorFields : DateOperationFields
    {
        public int? CreatedBy { get; set; }
        public OperatorType? CreatedUserType { get; set; }
        public int? LastModifiedBy { get; set; }
        public OperatorType? LastModifiedUserType { get; set; }
    }

    public interface IDateOperationFields
    {
        DateTime CreatedDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }

    [Serializable]
    public class DateOperationFields : IDateOperationFields
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
