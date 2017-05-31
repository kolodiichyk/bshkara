using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models.Base
{
    public class ApiBaseNamedModel : ApiBaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}