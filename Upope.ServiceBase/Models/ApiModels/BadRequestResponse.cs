using Newtonsoft.Json;

namespace Upope.ServiceBase.Models.ApiModels
{
    public class BadRequestResponse
    {
        public BadRequestResponse()
        {

        }

        public BadRequestResponse(Status status, object response)
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
