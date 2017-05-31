using System;
using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// Maid previous employment history
    /// </summary>
    [JsonObject("maid_employment_history")]
    public class ApiMaidEmploymentHistory : ApiBaseModel
    {
        /// <summary>
        /// <c>Country object</c>
        /// </summary>
        [JsonProperty("country")]
        public ApiCountry Country { get; set; }

        /// <summary>
        /// Employment duration
        /// </summary>
        [JsonProperty("duration")]
        public decimal Duration { get; set; }

        /// <summary>
        /// Employment description
        /// </summary>
        [JsonProperty("descripion")]
        public string Descripion { get; set; }
    }
}