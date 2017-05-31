using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    [JsonObject("document_type")]
    public class ApiDocumentType : ApiBaseNamedModel
    {
        [JsonProperty("show_as_picture")]
        public bool ShowAsPicture { get; set; }

        [JsonProperty("icon")]
        [JsonConverter(typeof(PhotoConverter))]
        public string Icon { get; set; }
    }
}
