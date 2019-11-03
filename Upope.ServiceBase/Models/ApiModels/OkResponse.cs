using Newtonsoft.Json;

namespace Upope.ServiceBase.Models.ApiModels
{
    public class OkResponse
    {
        public OkResponse()
        {
        
        }

        public OkResponse(Status status, object response)
        {
            Status = status;
            Response = response;
        }
        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public object Response { get; set; }
    }
}
