using System;
using System.Collections.Generic;
using System.Linq;
using Bashkra.ApiClient.Models.Base;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bashkra.ApiClient.Models
{
    /// <summary>
    /// <see cref="ApiMaid" /> profile
    /// </summary>
    [JsonObject("maid")]
    public class ApiMaid : ApiBaseNamedModel
    {
        /// <summary>
        /// Date of birthday
        /// </summary>
        [JsonProperty("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// <c>Maid</c> marital status
        /// </summary>
        [JsonProperty("marital_status")]
        public MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// Number of maid children
        /// </summary>
        [JsonProperty("no_of_children")]
        public int NoOfChildren { get; set; }

        /// <summary>
        /// <c>Maid</c> weight
        /// </summary>
        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        /// <summary>
        /// <c>Maid</c> height
        /// </summary>
        [JsonProperty("height")]
        public decimal Height { get; set; }

        /// <summary>
        /// <c>Maid</c> religion
        /// </summary>
        [JsonProperty("religion")]
        public Religion Religion { get; set; }

        /// <summary>
        /// Mail gender
        /// </summary>
        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        /// <summary>
        /// <c>Maid</c> education
        /// </summary>
        [JsonProperty("education")]
        public string Education { get; set; }

        /// <summary>
        /// Number of years Of experience
        /// </summary>
        [JsonProperty("years_of_experience")]
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// <c>Maid</c> phone number
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// <c>Maid</c> address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// <c>Maid</c> expected salary
        /// </summary>
        [JsonProperty("salary")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Is maid available now
        /// </summary>
        [JsonProperty("availability")]
        public bool Availability { get; set; }

        /// <summary>
        /// <c>Maid</c> photo
        /// </summary>
        [JsonProperty("photo")]
        [JsonConverter(typeof (PhotoConverter))]
        public string Photo { get; set; }

        /// <summary>
        /// <c>Description</c>
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// <see cref="Bashkra.ApiClient.Models.ApiMaid.Agency" /> where maid
        /// belongs
        /// </summary>
        [JsonProperty("agency")]
        public ApiAgency Agency { get; set; }

        /// <summary>
        /// City where maid live
        /// </summary>
        [JsonProperty("living_city")]
        public ApiCity LivingCity { get; set; }

        /// <summary>
        /// <see cref="ApiMaid" /> passport details
        /// </summary>
        [JsonProperty("maid_passport_detail")]
        public ApiMaidPassportDetails Passport { get; set; }

        /// <summary>
        /// Visa status
        /// </summary>
        [JsonProperty("visa_status")]
        public ApiVisaStatus VisaStatus { get; set; }

        /// <summary>
        /// <see cref="Bashkra.ApiClient.Models.ApiMaid.Nationality" />
        /// </summary>
        [JsonProperty("nationality")]
        public ApiNationality Nationality { get; set; }

        /// <summary>
        /// <see cref="ApiMaid" /> skills
        /// </summary>
        [JsonProperty("skills")]
        public List<ApiMaidSkill> Skills { get; set; }

        /// <summary>
        /// <see cref="ApiMaid" /> languages
        /// </summary>
        [JsonProperty("languages")]
        public List<ApiMaidLanguage> Languages { get; set; }

        /// <summary>
        /// <see cref="ApiMaid" /> employment history
        /// </summary>
        [JsonProperty("employment_history")]
        public List<ApiMaidEmploymentHistory> EmploymentHistory { get; set; }

        /// <summary>
        /// <see cref="ApiMaid" /> documents
        /// </summary>
        [JsonProperty("documents")]
        public List<ApiMaidDocument> Documents { get; set; }
    }

    public class PhotoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return Constants.BASE_URL + serializer.Deserialize<string>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject) t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }
    }
}