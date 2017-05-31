using System.Collections.Generic;
using System.Linq;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class MaidEmploymentHistoriesService :
        CRUDService<MaidEmploymentHistoryEntity, MaidEmploymentHistoriesViewModel>
    {
        public MaidEmploymentHistoriesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override MaidEmploymentHistoriesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new MaidEmploymentHistoriesViewModel
            {
                Title = BshkaraRes.MaidEmploymentHistories_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize,
                MaidId = args.MaidId
            };

            var query = UnitOfWork.Repository<MaidEmploymentHistoryEntity>().Query()
                .Include(x => x.Country)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Descripion.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Descripion.Ar.Contains(args.SearchString));
                        break;
                }
            }

            query.Filter(x => x.IsDeleted == false && (args.MaidId == null || x.MaidId == args.MaidId));
            query.OrderBy(q => q.OrderBy(d => d.Descripion.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<MaidEmploymentHistoryEntity>(items, args.PageNumber, args.PageSize,
                count);

            return viewModel;
        }

        public override string CanDeleteEntity(MaidEmploymentHistoryEntity entity)
        {
            if (UnitOfWork.Repository<MaidEntity>().Query().Filter(x => x.Passport.Id == entity.Id).Count() > 0)
            {
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;
            }

            if (
                UnitOfWork.Repository<MaidEntity>()
                    .Query()
                    .Filter(x => x.Passport.Id == entity.Id)
                    .Count() > 0)
            {
                return BshkaraRes.Countries_CantDeleteExistsInMaidEmploymentHistory;
            }

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                    $"select description{Lang} from MaidEmploymentHistory where isDeleted = 0 and description{Lang} like N'%{key}%' order by description{Lang}")
                    .ToList();
        }
    }
}