using Newtonsoft.Json;
using Upope.ServiceBase.Enums;

namespace Upope.ServiceBase.Interfaces
{
    public interface IHasCulturedEntityStatus
    {
        [JsonIgnore]
        Status CulturedEntityStatus { get; set; }
    }
}
