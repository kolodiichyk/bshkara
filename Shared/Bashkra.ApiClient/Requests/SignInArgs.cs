using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    /// <summary>
    /// Arguments to SignIn
    /// </summary>
    public class SignInArgs
    {
        /// <summary>
        /// User e-mail
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}