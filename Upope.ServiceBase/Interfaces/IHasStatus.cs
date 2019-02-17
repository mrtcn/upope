using Newtonsoft.Json;
using Upope.ServiceBase.Enums;

namespace Upope.ServiceBase.Interfaces
{
    public interface IHasStatus
    {
        [JsonIgnore]
        Status Status { get; set; }
    }
}
