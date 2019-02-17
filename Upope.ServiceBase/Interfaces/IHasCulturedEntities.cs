using System.Collections.Generic;

namespace Upope.ServiceBase.Interfaces
{
    public interface IHasCulturedEntities<TCulturedEntity>
    {
        ICollection<TCulturedEntity> CulturedEntities { get; set; }
    }
}
