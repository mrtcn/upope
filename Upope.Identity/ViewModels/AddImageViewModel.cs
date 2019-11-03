using Newtonsoft.Json;
using Upope.Identity.Helpers;

namespace Upope.Identity.ViewModels
{
    public class AddImageViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ImageData { get; set; }
    }
}
