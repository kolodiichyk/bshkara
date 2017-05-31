using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// <see cref="ApiCountry" /> />
    /// </summary>
    [JsonObject("country")]
    public class ApiCountry : ApiBaseNamedModel
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}