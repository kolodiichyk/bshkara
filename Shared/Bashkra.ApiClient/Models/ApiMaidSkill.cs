using Bashkra.ApiClient.Models.Base;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// What skills maid has
    /// </summary>
    [JsonObject("maid_skill")]
    public class ApiMaidSkill : ApiBaseModel
    {
        /// <summary>
        /// <c>Skill object</c>
        /// </summary>
        [JsonProperty("skill")]
        public ApiSkill Skill { get; set; }

        /// <summary>
        /// How good maid know skill
        /// </summary>
        [JsonProperty("skill_level")]
        public Level SkillLevel { get; set; }
    }
}