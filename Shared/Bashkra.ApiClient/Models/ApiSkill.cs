using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// Skills that maid can have
    /// </summary>
    [JsonObject("skill")]
    public class ApiSkill : ApiBaseNamedModel
    {
        [JsonProperty("icon")]
        [JsonConverter(typeof (PhotoConverter))]
        public string Icon { get; set; }
    }
}