using System.Collections.Generic;
using System.Threading.Tasks;
using Bashkra.ApiClient;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bshkara.Mobile.Helpers;

namespace Bshkara.Mobile.Services.WebService
{
    public class WebService : IWebService
    {
        private readonly BashkraApiClient _apiClient;

        public WebService()
        {
            _apiClient = new BashkraApiClient
            {
                ApiKey = "GJ9D0QN8n0KGHXlZ+J2byw==",
                AccessToken = Settings.AccessToken
            };
        }

        public async Task<SignInApiResponse> LogInAsync(string email, string password)
        {
            return await _apiClient.AuthSignIn(email, password);
        }


        public async Task<SignInApiResponse> SignUpAsync(SignUpArgs args)
        {
            return await _apiClient.AuthSignUp(args);
        }

        public async Task<ApiResponse> LogOutAsync()
        {
            return await _apiClient.AuthSignOut();
        }

        public async Task<IEnumerable<ApiMaid>> GetMaidsAsync(MaidsArgs args)
        {
            var apiMaidRespobse = await _apiClient.InfoMaidsAsync(args);

            if (!apiMaidRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiMaidRespobse.Error);
                return new List<ApiMaid>();
            }

            return apiMaidRespobse.Maids ?? new List<ApiMaid>();
        }

        public async Task<IEnumerable<ApiCountry>> GetCountriesAsync()
        {
            var apiCountriesRespobse = await _apiClient.InfoCountriesAsync();

            if (!apiCountriesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiCountriesRespobse.Error);
                return new List<ApiCountry>();
            }

            return apiCountriesRespobse.Countries ?? new List<ApiCountry>();
        }

        public async Task<IEnumerable<ApiNationality>> GetNationalitiesAsync(NationalitiesArgs args)
        {
            var apiNationalitiesRespobse = await _apiClient.InfoNationalitiesAsync(args);

            if (!apiNationalitiesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiNationalitiesRespobse.Error);
                return new List<ApiNationality>();
            }

            return apiNationalitiesRespobse.Nationalities ?? new List<ApiNationality>();
        }

        public async Task<IEnumerable<ApiCity>> GetCitiesAsync(CitiesArgs args)
        {
            var apiCitiesResponse = await _apiClient.InfoCitiesAsync(args);

            if (!apiCitiesResponse.Success)
            {
                App.Dialogs.ErrorToast("error", apiCitiesResponse.Error);
                return new List<ApiCity>();
            }

            return apiCitiesResponse.Cities ?? new List<ApiCity>();
        }

        public async Task<IEnumerable<ApiVisaStatus>> GetVisaStatusesAsync()
        {
            var apiVisaStatusesRespobse = await _apiClient.InfoVisaStatusesAsync();

            if (!apiVisaStatusesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiVisaStatusesRespobse.Error);
                return new List<ApiVisaStatus>();
            }

            return apiVisaStatusesRespobse.VisaStatuses ?? new List<ApiVisaStatus>();
        }

        public async Task<IEnumerable<ApiPackage>> GetPackagesAsync()
        {
            var apiPackagesRespobse = await _apiClient.InfoPackagesAsync();

            if (!apiPackagesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiPackagesRespobse.Error);
                return new List<ApiPackage>();
            }

            return apiPackagesRespobse.Packeges ?? new List<ApiPackage>();
        }

        public async Task<IEnumerable<ApiLanguage>> GetLanguagesAsync()
        {
            var apiLanguagesRespobse = await _apiClient.InfoLanguagesAsync();

            if (!apiLanguagesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiLanguagesRespobse.Error);
                return new List<ApiLanguage>();
            }

            return apiLanguagesRespobse.Languages ?? new List<ApiLanguage>();
        }

        public async Task<IEnumerable<ApiSkill>> GetSkillsAsync()
        {
            var apiSkillsRespobse = await _apiClient.InfoSkillsAsync();

            if (!apiSkillsRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiSkillsRespobse.Error);
                return new List<ApiSkill>();
            }

            return apiSkillsRespobse.Skills ?? new List<ApiSkill>();
        }

        public void ChangeAccessToken(string accessToken)
        {
            _apiClient.AccessToken = accessToken;
            Settings.AccessToken = accessToken;
        }

        public async Task<IEnumerable<ApiAgency>> GetAgenciesAsync(AgenciesArgs args)
        {
            var apiLanguagesRespobse = await _apiClient.InfoAgenciesAsync(args);

            if (!apiLanguagesRespobse.Success)
            {
                App.Dialogs.ErrorToast("error", apiLanguagesRespobse.Error);
                return new List<ApiAgency>();
            }

            return apiLanguagesRespobse.Agancies ?? new List<ApiAgency>();
        }

        public async Task<SignInApiResponse> LogInAsync(ExternalSignInArgs args)
        {
            return await _apiClient.ExternalSignIn(args);
        }
    }
}