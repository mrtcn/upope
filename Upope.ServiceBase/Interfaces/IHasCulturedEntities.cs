using System.Collections.Generic;

namespace Upope.ServiceBase.Interfaces
{
    public interface IHasCulturedEntities<TCulturedEntity>
    {
        List<TCulturedEntity> CulturedEntities { get; set; }
    }
}
