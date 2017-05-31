using Bashkra.ApiClient.Models;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    public class SignInApiResponse : ApiResponse
    {
        [JsonProperty("sign_in_status")]
        public SignInStatus SignInStatus { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("user")]
        public ApiUser User { get; set; }
    }
}