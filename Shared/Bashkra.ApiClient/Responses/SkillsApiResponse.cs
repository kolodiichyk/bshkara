using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Responses
{
    [JsonObject("skills")]
    public class SkillsApiResponse : ApiResponse
    {
        public SkillsApiResponse()
        {
            Skills = new List<ApiSkill>();
        }

        [JsonProperty("skills")]
        public IList<ApiSkill> Skills { get; set; }
    }
}