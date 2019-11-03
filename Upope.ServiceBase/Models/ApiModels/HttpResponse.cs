
using Newtonsoft.Json;

namespace Upope.ServiceBase.Models.ApiModels
{
    public class HttpResponse
    {
        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public object Response { get; set; }
    }
}
