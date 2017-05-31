using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("visa_statuses")]
    public class VisaStatusesApiResponse : ApiResponse
    {
        public VisaStatusesApiResponse()
        {
            VisaStatuses = new List<ApiVisaStatus>();
        }

        [JsonProperty("visa_statuses")]
        public IList<ApiVisaStatus> VisaStatuses { get; set; }
    }
}