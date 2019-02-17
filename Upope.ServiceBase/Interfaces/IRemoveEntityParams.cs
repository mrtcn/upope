using Upope.ServiceBase.ServiceBase.Models;

namespace Upope.ServiceBase.Interfaces
{
    public interface IRemoveEntityParams : IHasOperator, IHasCulture, IEntity
    {
        bool CheckRelationalEntities { get; set; }
        bool RemovePermanently { get; set; }
    }
}
