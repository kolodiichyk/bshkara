using System;
using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// What languages maid know
    /// </summary>
    [JsonObject("maid_passport_details")]
    public class ApiMaidPassportDetails : ApiBaseModel
    {
        /// <summary>
        /// Number of maid passport
        /// </summary>
        [JsonProperty("passport_number")]
        public string PassportNumber { get; set; }

        /// <summary>
        /// Passport issue date
        /// </summary>
        [JsonProperty("issue_date")]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Passport expiration date
        /// </summary>
        [JsonProperty("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Passport issue place
        /// </summary>
        [JsonProperty("issue_place")]
        public string IssuePlace { get; set; }
    }
}