using Newtonsoft.Json;

namespace Upope.Identity.Models.FacebookResponse
{
    public class FacebookResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "age_range")]
        public string AgeRange { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public string Birthday { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public Picture Picture { get; set; }

        [JsonProperty(PropertyName = "largePictureUrl")]
        public string LargePictureUrl { get; set; }
    }
}

public class Picture
{
    [JsonProperty(PropertyName = "data")]
    public Data Data { get; set; }
}

public class Data
{
    [JsonProperty(PropertyName = "height")]
    public int Height  { get; set; }
    [JsonProperty(PropertyName = "is_silhouette")]
    public bool IsSilhouette { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
    [JsonProperty(PropertyName = "width")]
    public int Width { get; set; }
}