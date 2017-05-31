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
    public class AgenciesService : CRUDService<AgencyEntity, AgenciesViewModel>
    {
        public AgenciesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public override AgenciesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new AgenciesViewModel
            {
                Title = BshkaraRes.Agencies_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<AgencyEntity>().Query()
                .Include(x => x.City)
                .Include(x => x.Packages)
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

            viewModel.Items = new StaticPagedList<AgencyEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(AgencyEntity entity)
        {
            if (UnitOfWork.Repository<AgencyPackageEntity>().Query().Filter(x => x.AgencyId == entity.Id).Count() > 0)
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                        $"select name{Lang} from maids where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }


        public IEnumerable<IdValueModel> GetAgenciesIdsValues()
        {
            var countries = UnitOfWork.Context.Set<AgencyEntity>().Where(t => !t.IsDeleted).ToList();
            return countries.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }

        public List<ApiAgency> GetAgenciesForApi(AgenciesArgs args, out int total)
        {
            var culture = CultureHelper.GetCurrentCulture();
            Expression<Func<AgencyEntity, bool>> searchPredicate = agency => true;
            Expression<Func<AgencyEntity, bool>> idPredicate = agency => true;
            Expression<Func<AgencyEntity, bool>> notDeleted = agency => !agency.IsDeleted;

            if (args.Id != null)
                idPredicate = agency => agency.Id == args.Id;

            if (!string.IsNullOrWhiteSpace(args.Search))
                if (culture == "ar")
                    searchPredicate = city => city.Name.Ar.Contains(args.Search);
                else
                    searchPredicate = city => city.Name.En.Contains(args.Search);

            total = UnitOfWork.Context.Set<AgencyEntity>()
                .Where(idPredicate)
                .Where(searchPredicate)
                .Where(notDeleted)
                .Count();

            var agencies =
                UnitOfWork.Context.Set<AgencyEntity>()
                    .Include(agency => agency.City)
                    .Include(agency => agency.City.Country)
                    .Where(idPredicate)
                    .Where(searchPredicate)
                    .Where(notDeleted)
                    .OrderBy(agency => agency.Name.En)
                    .Skip(args.Paging.PageNumber*args.Paging.PageSize)
                    .Take(args.Paging.PageSize == 0 ? total : args.Paging.PageSize)
                    .ToList();

            var list = agencies.Select(agency => new ApiAgency
            {
                Id = agency.Id,
                Name = agency.Name.Default,
                Logo = agency.Logo,
                Address = agency.Address.Default,
                TradeLicenseNumber = agency.TradeLicenseNumber,
                Email = agency.Email,
                Phone = agency.City != null ? "1" : "0" /*agency.Phone*/,
                Mobile = agency.Mobile,
                City = agency.City != null
                    ? new ApiCity
                    {
                        Id = agency.City.Id,
                        Name = agency.City.Name?.Default,
                        CountryId = agency.City.CountryId,
                        Country = agency.City.Country != null
                            ? new ApiCountry
                            {
                                Id = agency.City.CountryId,
                                CountryCode = agency.City.Country.CountryCode,
                                Name = agency.City.Country.Name.Default
                            }
                            : null
                    }
                    : null
            }).ToList();

            return list;
        }
    }
}