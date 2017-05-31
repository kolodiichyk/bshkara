using System;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Models.Base
{
    /// <summary>
    /// Audited dictionary
    /// </summary>
    public abstract class ApiBaseModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}