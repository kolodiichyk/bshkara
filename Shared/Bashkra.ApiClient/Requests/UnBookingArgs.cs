using System;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    public class UnBookingArgs
    {
        [JsonProperty("maid_Id")]
        public Guid MaidId { get; set; }

        [JsonProperty("user_Id")]
        public Guid UserId { get; set; }
    }
}