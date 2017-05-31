using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("cities")]
    public class CitiesApiResponse : ApiResponse
    {
        public CitiesApiResponse()
        {
            Cities = new List<ApiCity>();
            Paging = new ApiPaging();
        }

        [JsonProperty("cities")]
        public IList<ApiCity> Cities { get; set; }

        [JsonProperty("paging")]
        public ApiPaging Paging { get; set; }
    }
}