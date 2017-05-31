using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("languages")]
    public class LanguagesApiResponse : ApiResponse
    {
        public LanguagesApiResponse()
        {
            Languages = new List<ApiLanguage>();
        }

        [JsonProperty("languages")]
        public IList<ApiLanguage> Languages { get; set; }
    }
}