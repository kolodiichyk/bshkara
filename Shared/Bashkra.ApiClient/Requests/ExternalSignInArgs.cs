using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    public class ExternalSignInArgs
    {
        /// <summary>
        ///     External provider type
        /// </summary>
        [JsonProperty("provider")]
        public LoginProvider LoginProvider { get; set; }

        /// <summary>
        ///     User key in provider
        /// </summary>
        [JsonProperty("provider_key")]
        public string ProviderKey { get; set; }

        /// <summary>
        ///     External provider token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        ///     User e-mail
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        ///     User e-mail
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}