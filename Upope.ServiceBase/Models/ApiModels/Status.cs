using Newtonsoft.Json;

namespace Upope.ServiceBase.Models.ApiModels
{
    public class Status
    {
        public Status()
        {

        }

        public Status(int code)
        {         
            Code = code;
        }

        public Status(int code, string message)
        {
            Code = code;
            Message = message;
        }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
