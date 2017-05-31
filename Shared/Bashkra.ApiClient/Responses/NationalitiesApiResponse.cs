using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("nationalities")]
    public class NationalitiesApiResponse : ApiResponse
    {
        public NationalitiesApiResponse()
        {
            Nationalities = new List<ApiNationality>();
            Paging = new ApiPaging();
        }

        [JsonProperty("nationalities")]
        public IList<ApiNationality> Nationalities { get; set; }

        [JsonProperty("paging")]
        public ApiPaging Paging { get; set; }
    }
}