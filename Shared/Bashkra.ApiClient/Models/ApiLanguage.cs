using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// <see cref="ApiLanguage" /> that maid can know
    /// </summary>
    [JsonObject("language")]
    public class ApiLanguage : ApiBaseNamedModel
    {
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
    }
}