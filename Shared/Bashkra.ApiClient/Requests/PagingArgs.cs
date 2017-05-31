using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    [JsonObject("paging")]
    public class PagingArgs
    {
        [JsonProperty("page_number")]
        public int PageNumber { get; set; } = 0;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 25;
    }
}