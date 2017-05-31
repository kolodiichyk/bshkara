using Bashkra.ApiClient.Models.Base;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// Skills that maid can have
    /// </summary>
    [JsonObject("user")]
    public class ApiUser : ApiBaseModel
    {
        /// <summary>
        /// <see cref="ApiUser" /> name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// <see cref="ApiUser" /> mobile number
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}