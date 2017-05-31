using System;
using System.Collections.Generic;
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
    public class NationalitiesService : CRUDService<NationalityEntity, NationalitiesViewModel>
    {
        public NationalitiesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override NationalitiesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new NationalitiesViewModel
            {
                Title = BshkaraRes.Nationalities_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<NationalityEntity>().Query()
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

            viewModel.Items = new StaticPagedList<NationalityEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(NationalityEntity entity)
        {
            if (UnitOfWork.Repository<MaidEntity>().Query().Filter(x => x.NationalityId == entity.Id).Count() > 0)
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;

            if (
                UnitOfWork.Repository<MaidEntity>()
                    .Query()
                    .Filter(x => x.NationalityId == entity.Id)
                    .Count() > 0)
                return BshkaraRes.Countries_CantDeleteExistsInMaidEmploymentHistory;

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                        $"select name{Lang} from nationalities where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }

        public IEnumerable<IdValueModel> GetNationalitiesIdsValues()
        {
            var nationalitis =
                UnitOfWork.Context.Set<NationalityEntity>().Where(t => !t.IsDeleted).ToList();
            return nationalitis.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }


        public List<ApiNationality> GetNationalitiesForApi(NationalitiesArgs args, out int total)
        {
            var culture = CultureHelper.GetCurrentCulture();
            Expression<Func<NationalityEntity, bool>> searchPredicate = nationality => true;
            Expression<Func<NationalityEntity, bool>> idPredicate = nationality => true;
            Expression<Func<NationalityEntity, bool>> notDeleted = nationality => !nationality.IsDeleted;

            if (args.Id != null)
                idPredicate = nationality => nationality.Id == args.Id;

            if (!string.IsNullOrWhiteSpace(args.Search))
                if (culture == "ar")
                    searchPredicate = nationality => nationality.Name.Ar.Contains(args.Search);
                else
                    searchPredicate = nationality => nationality.Name.En.Contains(args.Search);

            total = UnitOfWork.Context.Set<NationalityEntity>()
                .Where(idPredicate)
                .Where(searchPredicate)
                .Where(notDeleted)
                .Count();

            var nationalities =
                UnitOfWork.Context.Set<NationalityEntity>()
                    .Where(idPredicate)
                    .Where(searchPredicate)
                    .Where(notDeleted)
                    .OrderBy(nationality => nationality.Id)
                    .Skip(args.Paging.PageNumber*args.Paging.PageSize)
                    .Take(args.Paging.PageSize == 0 ? total : args.Paging.PageSize)
                    .ToList();

            var list = nationalities.Select(nationality => new ApiNationality
            {
                Id = nationality.Id,
                Name = nationality.Name.Default
            }).ToList();

            return list;
        }
    }
}