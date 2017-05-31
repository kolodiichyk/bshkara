using System.Collections.Generic;
using System.Linq;
using Bashkra.ApiClient.Models;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class CountriesService : CRUDService<CountryEntity, CountriesViewModel>
    {
        public CountriesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<IdValueModel> GetCountriesIdsValues()
        {
            var countries = UnitOfWork.Context.Set<CountryEntity>().Where(t => !t.IsDeleted).ToList();
            return countries.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }

        public override CountriesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new CountriesViewModel
            {
                Title = BshkaraRes.Countries_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<CountryEntity>().Query()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Name.Ar.Contains(args.SearchString));
                        break;
                }

            query.Filter(x => x.IsDeleted == false);

            query.OrderBy(q => q.OrderBy(d => d.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<CountryEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(CountryEntity entity)
        {
            if (UnitOfWork.Repository<CityEntity>().Query().Filter(x => x.CountryId == entity.Id).Count() > 0)
                return BshkaraRes.Countries_CantDeleteExistsInAgencies;

            if (
                UnitOfWork.Repository<MaidEmploymentHistoryEntity>()
                    .Query()
                    .Filter(x => x.CountryId == entity.Id)
                    .Count() > 0)
                return BshkaraRes.Countries_CantDeleteExistsInMaidEmploymentHistory;

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                        $"select name{Lang} from countries where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }

        public List<ApiCountry> GetCountriesForApi()
        {
            var countries = UnitOfWork.Context.Set<CountryEntity>().Where(t => !t.IsDeleted).ToList();

            var list = countries.Select(country => new ApiCountry
            {
                Id = country.Id,
                CountryCode = country.CountryCode,
                Name = country.Name.Default
            }).ToList();

            return list;
        }
    }
}