using System.Collections.Generic;
using System.Threading.Tasks;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;

namespace Bshkara.Mobile.Services.WebService
{
    public interface IWebService
    {
        void ChangeAccessToken(string accessToken);

        Task<SignInApiResponse> LogInAsync(ExternalSignInArgs args);

        Task<SignInApiResponse> LogInAsync(string email, string password);

        Task<SignInApiResponse> SignUpAsync(SignUpArgs args);

        Task<ApiResponse> LogOutAsync();

        Task<IEnumerable<ApiMaid>> GetMaidsAsync(MaidsArgs args);

        Task<IEnumerable<ApiCountry>> GetCountriesAsync();

        Task<IEnumerable<ApiLanguage>> GetLanguagesAsync();

        Task<IEnumerable<ApiSkill>> GetSkillsAsync();

        Task<IEnumerable<ApiNationality>> GetNationalitiesAsync(NationalitiesArgs args);

        Task<IEnumerable<ApiCity>> GetCitiesAsync(CitiesArgs args);

        Task<IEnumerable<ApiVisaStatus>> GetVisaStatusesAsync();

        Task<IEnumerable<ApiPackage>> GetPackagesAsync();

        Task<IEnumerable<ApiAgency>> GetAgenciesAsync(AgenciesArgs args);
    }
}