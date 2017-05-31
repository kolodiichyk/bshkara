using System;
using System.Collections.Generic;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient.Requests
{
    public class MaidsArgs : BaseArgs
    {
        public MaidsArgs()
        {
            Maids = new List<Guid>();
            Skills = new List<Guid>();
            Languages = new List<Guid>();
            Religions = new List<Religion>();
        }

        [JsonProperty("maids_id_list")]
        public List<Guid> Maids { get; set; }

        [JsonProperty("skills_id_list")]
        public List<Guid> Skills { get; set; }

        [JsonProperty("languages_id_list")]
        public List<Guid> Languages { get; set; }

        [JsonProperty("min_salary")]
        public decimal? MinSalary { get; set; }

        [JsonProperty("max_salary")]
        public decimal? MaxSalary { get; set; }

        [JsonProperty("min_age")]
        public decimal? MinAge { get; set; }

        [JsonProperty("max_age")]
        public decimal? MaxAge { get; set; }

        [JsonProperty("min_years_of_experience")]
        public decimal? MinYearsOfExperience { get; set; }

        [JsonProperty("max_years_of_experience")]
        public decimal? MaxYearsOfExperience { get; set; }

        [JsonProperty("gender")]
        public Gender? Gender { get; set; }

        [JsonProperty("religions")]
        public List<Religion> Religions { get; set; }

        [JsonProperty("marital_status")]
        public MaritalStatus? MaritalStatus { get; set; }

        [JsonProperty("visa_status")]
        public Guid? VisaStatus { get; set; }

        [JsonProperty("nationality")]
        public Guid? Nationality { get; set; }

        [JsonProperty("agency")]
        public Guid? Agency { get; set; }

        [JsonProperty("city")]
        public Guid? City { get; set; }

        [JsonProperty("availability")]
        public bool? Availability { get; set; }

        [JsonProperty("only_with_photos")]
        public bool? OnlyWithPhotos { get; set; }
    }
}