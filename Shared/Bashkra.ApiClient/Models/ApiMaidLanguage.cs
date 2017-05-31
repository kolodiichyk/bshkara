using Bashkra.ApiClient.Models.Base;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// What languages maid know
    /// </summary>
    [JsonObject("maid_language")]
    public class ApiMaidLanguage : ApiBaseModel
    {
        /// <summary>
        /// <c>Language object</c>
        /// </summary>
        [JsonProperty("language")]
        public ApiLanguage Language { get; set; }

        [JsonProperty("spoken_level")]
        public Level SpokenLevel { get; set; }

        [JsonProperty("read_level")]
        public Level ReadLevel { get; set; }

        [JsonProperty("written_level")]
        public Level WrittenLevel { get; set; }
    }
}