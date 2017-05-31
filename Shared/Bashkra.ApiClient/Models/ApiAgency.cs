using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    ///     <see cref="ApiAgency" />
    /// </summary>
    [JsonObject("agency")]
    public class ApiAgency : ApiBaseNamedModel
    {
        /// <summary>
        ///     <see cref="Bashkra.ApiClient.Models.ApiAgency.City" /> where agency
        ///     placed
        /// </summary>
        [JsonProperty("city")]
        public virtual ApiCity City { get; set; }

        /// <summary>
        ///     <see cref="Bashkra.ApiClient.Models.ApiAgency.Logo" /> of agency
        /// </summary>
        [JsonProperty("logo")]
        [JsonConverter(typeof(PhotoConverter))]
        public string Logo { get; set; }

        /// <summary>
        ///     Trade license number of agency
        /// </summary>
        [JsonProperty("trade_license_number")]
        public string TradeLicenseNumber { get; set; }

        /// <summary>
        ///     <see cref="ApiAgency" /> web site
        /// </summary>
        [JsonProperty("website")]
        public string Website { get; set; }

        /// <summary>
        ///     <see cref="ApiAgency" /> email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     <see cref="ApiAgency" /> address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        ///     <see cref="ApiAgency" /> phone number
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        ///     <see cref="ApiAgency" /> mobile phone number
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }


        /// <summary>
        ///     <see cref="ApiAgency" /> location
        /// </summary>
        [JsonProperty("location")]
        public string Location => City != null ? $"{City?.Name}, {City?.Country?.Name}" : null;
    }
}