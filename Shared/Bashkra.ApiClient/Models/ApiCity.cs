using System;
using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    ///     City
    /// </summary>
    [JsonObject("city")]
    public class ApiCity : ApiBaseNamedModel
    {
        [JsonProperty("country_id")]
        public Guid CountryId { get; set; }

        [JsonProperty("country")]
        public ApiCountry Country { get; set; }

        [JsonIgnore]
        public string CityWithCountry => $"{Name}, {Country?.Name}";
    }
}