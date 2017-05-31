using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("countries")]
    public class CountriesApiResponse : ApiResponse
    {
        public CountriesApiResponse()
        {
            Countries = new List<ApiCountry>();
        }

        [JsonProperty("countries")]
        public IList<ApiCountry> Countries { get; set; }
    }
}