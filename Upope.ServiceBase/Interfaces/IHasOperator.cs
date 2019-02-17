using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.ServiceBase.Interfaces
{
    public interface IHasOperator
    {
        int OperatorId { get; set; }
        OperatorType OperatorType { get; set; }
    }
}
