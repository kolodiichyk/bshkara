using System;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    [JsonObject("base")]
    public class BaseArgs
    {
        public BaseArgs()
        {
            Paging = new PagingArgs();
        }

        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("paging")]
        public PagingArgs Paging { get; set; }
    }
}