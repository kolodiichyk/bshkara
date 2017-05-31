using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    public class BookingArgs : UnBookingArgs
    {
        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}