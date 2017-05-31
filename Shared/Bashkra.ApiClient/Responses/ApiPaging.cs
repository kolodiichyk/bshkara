using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    public class ApiPaging
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("page_number")]
        public int PageNumber { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }
    }
}
