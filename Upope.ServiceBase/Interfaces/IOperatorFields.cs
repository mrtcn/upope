using System;

namespace Upope.ServiceBase.Interfaces
{
    public interface IOperatorFields {
        DateTime CreatedDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
