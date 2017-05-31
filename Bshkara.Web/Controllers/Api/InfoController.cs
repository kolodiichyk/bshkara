using System.Web.Mvc;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bshkara.Web.Services;

namespace Bshkara.Web.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class InfoController : BaseApiController
    {
        private CountriesService _countriesService => DependencyResolver.Current.GetService<CountriesService>();

        private LanguagesService _languagesService => DependencyResolver.Current.GetService<LanguagesService>();

        private SkillsService _skillsService => DependencyResolver.Current.GetService<SkillsService>();

        private VisaStatusService _visaStatusService => DependencyResolver.Current.GetService<VisaStatusService>();

        private PackagesService _packagesService => DependencyResolver.Current.GetService<PackagesService>();

        private MaidsService _maidsService => DependencyResolver.Current.GetService<MaidsService>();

        private NationalitiesService _nationalitiesService
            => DependencyResolver.Current.GetService<NationalitiesService>();

        private CitiesService _citiesService
            => DependencyResolver.Current.GetService<CitiesService>();

        private AgenciesService _agenciesService
            => DependencyResolver.Current.GetService<AgenciesService>();

        /// <summary>
        ///     Get all countries
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("countries")]
        public CountriesApiResponse GetCountries()
        {
            return TryInvoce(() =>
            {
                var result = new CountriesApiResponse {Lang = Language};
                var countries = _countriesService.GetCountriesForApi();
                result.Countries = countries;
                return result;
            });
        }

        /// <summary>
        ///     Get all languages
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("languages")]
        public LanguagesApiResponse GetLanguages()
        {
            return TryInvoce(() =>
            {
                var result = new LanguagesApiResponse {Lang = Language};
                var languages = _languagesService.GetLanguagesForApi();
                result.Languages = languages;
                return result;
            });
        }

        /// <summary>
        ///     Get all skills
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("skills")]
        public SkillsApiResponse GetSkills()
        {
            return TryInvoce(() =>
            {
                var result = new SkillsApiResponse {Lang = Language};
                var skills = _skillsService.GetSkillsForApi();
                result.Skills = skills;
                return result;
            });
        }

        /// <summary>
        ///     Get all skills
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("nationalities")]
        public NationalitiesApiResponse GetNationalities(NationalitiesArgs args)
        {
            return TryInvoce(() =>
            {
                var result = new NationalitiesApiResponse {Lang = Language};
                int total;
                var nationalities = _nationalitiesService.GetNationalitiesForApi(args, out total);

                result.Paging.TotalCount = total;
                result.Paging.PageNumber = args.Paging.PageNumber;
                result.Paging.PageSize = args.Paging.PageSize;
                result.Nationalities = nationalities;

                return result;
            });
        }

        /// <summary>
        ///     Get all skills
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("cities")]
        public CitiesApiResponse GetCities(CitiesArgs args)
        {
            return TryInvoce(() =>
            {
                var result = new CitiesApiResponse {Lang = Language};
                int total;
                var cities = _citiesService.GetCitiesForApi(args, out total);

                result.Paging.TotalCount = total;
                result.Paging.PageNumber = args.Paging.PageNumber;
                result.Paging.PageSize = args.Paging.PageSize;
                result.Cities = cities;

                return result;
            });
        }

        /// <summary>
        ///     Get all skills
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("agencies")]
        public AgenciesApiResponse GetAgencies(AgenciesArgs args)
        {
            return TryInvoce(() =>
            {
                var result = new AgenciesApiResponse {Lang = Language};
                int total;
                var agancies = _agenciesService.GetAgenciesForApi(args, out total);

                result.Paging.TotalCount = total;
                result.Paging.PageNumber = args.Paging.PageNumber;
                result.Paging.PageSize = args.Paging.PageSize;
                result.Agancies = agancies;

                return result;
            });
        }

        /// <summary>
        ///     Get all visa statuses
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("visa_statuses")]
        public VisaStatusesApiResponse GetVisaStatuses()
        {
            return TryInvoce(() =>
            {
                var result = new VisaStatusesApiResponse {Lang = Language};
                var visaStatuses = _visaStatusService.GetVisaStatusesForApi();
                result.VisaStatuses = visaStatuses;
                return result;
            });
        }

        /// <summary>
        ///     Get all visa statuses
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("packages")]
        public PackagesApiResponse GetPackages()
        {
            return TryInvoce(() =>
            {
                var result = new PackagesApiResponse {Lang = Language};
                var packages = _packagesService.GetPackagesForApi();
                result.Packeges = packages;
                return result;
            });
        }

        /// <summary>
        ///     Get all maids
        /// </summary>
        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("maids")]
        public MaidsApiResponse GetMaids(MaidsArgs args)
        {
            return TryInvoce(() =>
            {
                var result = new MaidsApiResponse {Lang = Language};
                var total = 0;
                var maids = _maidsService.GetMaidsForApi(args, out total);
                result.Maids = maids;
                result.Paging.TotalCount = total;
                result.Paging.PageNumber = args.Paging.PageNumber;
                result.Paging.PageSize = args.Paging.PageSize;

                return result;
            });
        }
    }
}