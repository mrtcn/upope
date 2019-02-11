
using Newtonsoft.Json;

namespace Upope.Identity.Models
{
    public interface IExternalAuthResponse
    {
        [JsonProperty(PropertyName = "name")]
        string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        string Email { get; set; }

        [JsonProperty(PropertyName = "gender")]
        string Gender { get; set; }

        [JsonProperty(PropertyName = "locale")]
        string Locale { get; set; }
    }
}
