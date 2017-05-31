using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    public class SignUpArgs : SignInArgs
    {
        [JsonProperty("confirmated_password")]
        public string ConfirmedPassword { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}