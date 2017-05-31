using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("agencies")]
    public class AgenciesApiResponse : ApiResponse
    {
        public AgenciesApiResponse()
        {
            Paging = new ApiPaging();
        }

        [JsonProperty("agencies")]
        public IEnumerable<ApiAgency> Agancies { get; set; }

        [JsonProperty("paging")]
        public ApiPaging Paging { get; set; }
    }
}