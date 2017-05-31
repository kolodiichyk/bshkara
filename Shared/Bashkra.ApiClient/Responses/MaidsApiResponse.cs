using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("maids")]
    public class MaidsApiResponse : ApiResponse
    {
        public MaidsApiResponse()
        {
            Paging = new ApiPaging();
        }

        [JsonProperty("maids")]
        public IEnumerable<ApiMaid> Maids { get; set; }

        [JsonProperty("paging")]
        public ApiPaging Paging { get; set; }
    }
}