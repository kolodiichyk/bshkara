using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("packages")]
    public class PackagesApiResponse : ApiResponse
    {
        public PackagesApiResponse()
        {
            Packeges = new List<ApiPackage>();
        }

        [JsonProperty("packages")]
        public IList<ApiPackage> Packeges { get; set; }
    }
}