using System.Collections.Generic;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bshkara.Mobile.ViewModels.Base;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Bshkara.Mobile.Helpers
{
    public static class Settings
    {
        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(AccessTokenKey, null);
            }
            set { AppSettings.AddOrUpdateValue(AccessTokenKey, value); }
        }

        public static ApiUser User
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(UserKey);
                return string.IsNullOrWhiteSpace(value) ? null : JsonConvert.DeserializeObject<ApiUser>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(UserKey, JsonConvert.SerializeObject(value)); }
        }

        public static MaidsArgs MaidFilter
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(MaidFilterKey);
                return string.IsNullOrWhiteSpace(value)
                    ? new MaidsArgs()
                    : JsonConvert.DeserializeObject<MaidsArgs>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(MaidFilterKey, JsonConvert.SerializeObject(value)); }
        }

        public static IEnumerable<Selectable<ApiSkill>> SkillsFilter
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(SkillsFilterKey);
                return string.IsNullOrWhiteSpace(value)
                    ? new List<Selectable<ApiSkill>>()
                    : JsonConvert.DeserializeObject<IEnumerable<Selectable<ApiSkill>>>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(SkillsFilterKey, JsonConvert.SerializeObject(value)); }
        }

        public static IEnumerable<Selectable<ApiLanguage>> LanguagesFilter
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(LanguagesFilterKey);
                return string.IsNullOrWhiteSpace(value)
                    ? new List<Selectable<ApiLanguage>>()
                    : JsonConvert.DeserializeObject<IEnumerable<Selectable<ApiLanguage>>>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(LanguagesFilterKey, JsonConvert.SerializeObject(value)); }
        }

        public static ApiNationality NationalityFilter
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(NationalityFilterKey);
                return string.IsNullOrWhiteSpace(value)
                    ? null
                    : JsonConvert.DeserializeObject<ApiNationality>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(NationalityFilterKey, JsonConvert.SerializeObject(value)); }
        }

        public static ApiCity CityFilter
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(CityFilterKey);
                return string.IsNullOrWhiteSpace(value)
                    ? null
                    : JsonConvert.DeserializeObject<ApiCity>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(CityFilterKey, JsonConvert.SerializeObject(value)); }
        }


        public static bool IsFirstRun
        {
            get { return AppSettings.GetValueOrDefault(IsFirstRunKey, true); }
            set { AppSettings.AddOrUpdateValue(IsFirstRunKey, value); }
        }

        public static ApiCity UserLocation
        {
            get
            {
                var value = AppSettings.GetValueOrDefault<string>(UserLocationKey);
                return string.IsNullOrWhiteSpace(value)
                    ? null
                    : JsonConvert.DeserializeObject<ApiCity>(value, _jsonSettings);
            }
            set { AppSettings.AddOrUpdateValue(UserLocationKey, JsonConvert.SerializeObject(value)); }
        }

        #region Setting Constants

        private const string AccessTokenKey = "access_token";

        private const string UserKey = "user_token";

        private const string MaidFilterKey = " maids_filtter";

        private const string SkillsFilterKey = "skills_filtter";

        private const string LanguagesFilterKey = "languages_filtter";

        private const string NationalityFilterKey = "nationality_filtter";

        private const string CityFilterKey = "city_filtter";

        private const string IsFirstRunKey = "is_first_run";

        private const string UserLocationKey = "user_location";

        #endregion
    }
}