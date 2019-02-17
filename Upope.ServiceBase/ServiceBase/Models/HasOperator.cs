using Upope.ServiceBase.Interfaces;

namespace Upope.ServiceBase.ServiceBase.Models
{
    public class HasOperator : IHasOperator
    {
        public int OperatorId { get; set; }
        public OperatorType OperatorType { get; set; }
    }
}
