using System;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("response")]
    public class ApiResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("success")]
        public bool Success
        {
            get { return string.IsNullOrWhiteSpace(Error); }
        }

        [JsonProperty("execution_time")]
        public TimeSpan ExecutionTime { get; set; }

        [JsonProperty("lang")]
        public Language Lang { get; set; }

        [JsonProperty("server_timestamp")]
        public double ServerTimestamp { get; set; }
    }
}