using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class CitiesService : CRUDService<CityEntity, CitiesViewModel>
    {
        public CitiesService(IUnitOfWork unitOfWork) : base(unitOfWork)
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

        public override CitiesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new CitiesViewModel
            {
                Title = BshkaraRes.Cities_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<CityEntity>().Query()
                .Include(x => x.Country)
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

            viewModel.Items = new StaticPagedList<CityEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }


        public override string CanDeleteEntity(CityEntity entity)
        {
            if (UnitOfWork.Repository<MaidEntity>().Query().Filter(x => x.LivingCityId == entity.Id).Count() > 0)
                return BshkaraRes.Cities_CantDeleteExistsInMaid;

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                        $"select name{Lang} from cities where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }

        public IEnumerable<IdValueModel> GetCitiesIdsValues()
        {
            var countries =
                UnitOfWork.Context.Set<CityEntity>().Where(t => !t.IsDeleted).Include(t => t.Country).ToList();
            return countries.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = $"{x.Name.Default} ({x.Country.Name.Default})"
            }).OrderBy(x => x.Value);
        }

        public List<ApiCity> GetCitiesForApi(CitiesArgs args, out int total)
        {
            var culture = CultureHelper.GetCurrentCulture();
            Expression<Func<CityEntity, bool>> searchPredicate = city => true;
            Expression<Func<CityEntity, bool>> idPredicate = city => true;
            Expression<Func<CityEntity, bool>> notDeleted = city => !city.IsDeleted;

            if (args.Id != null)
                idPredicate = city => city.Id == args.Id;

            if (!string.IsNullOrWhiteSpace(args.Search))
                if (culture == "ar")
                    searchPredicate = city => city.Name.Ar.Contains(args.Search);
                else
                    searchPredicate = city => city.Name.En.Contains(args.Search);

            total = UnitOfWork.Context.Set<CityEntity>()
                .Where(idPredicate)
                .Where(searchPredicate)
                .Where(notDeleted)
                .Count();

            var nationalities =
                UnitOfWork.Context.Set<CityEntity>()
                    .Where(idPredicate)
                    .Where(searchPredicate)
                    .Where(notDeleted)
                    .OrderBy(city => city.Name.En)
                    .Skip(args.Paging.PageNumber*args.Paging.PageSize)
                    .Take(args.Paging.PageSize == 0 ? total : args.Paging.PageSize)
                    .ToList();

            var list = nationalities.Select(city => new ApiCity
            {
                Id = city.Id,
                Name = city.Name.Default,
                CountryId = city.CountryId,
                Country = city.Country != null
                    ? new ApiCountry
                    {
                        Id = city.CountryId,
                        CountryCode = city.Country.CountryCode,
                        Name = city.Country.Name?.Default
                    }
                    : null
            }).ToList();

            return list;
        }
    }
}