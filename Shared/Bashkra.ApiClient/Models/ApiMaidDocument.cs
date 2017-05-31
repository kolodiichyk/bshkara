using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// Maid Document files
    /// </summary>
    [JsonObject("maid_document")]
    public class ApiMaidDocument : ApiBaseModel
    {
        /// <summary>
        /// Path to file
        /// </summary>
        [JsonProperty("file")]
        [JsonConverter(typeof(PhotoConverter))]
        public string File { get; set; }

        /// <summary>
        /// <c>File</c> type
        /// </summary>
        [JsonProperty("document_type")]
        public ApiDocumentType DocumentType { get; set; }

        /// <summary>
        /// <c>File</c> format
        /// </summary>
        [JsonProperty("document_format")]
        public string DocumentFormat { get; set; }
    }
}