using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// <see cref="ApiPackage" />
    /// </summary>
    [JsonObject("package")]
    public class ApiPackage : ApiBaseNamedModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("users_count")]
        public int UsersCount { get; set; }

        [JsonProperty("listing_count")]
        public int ListingCount { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
}