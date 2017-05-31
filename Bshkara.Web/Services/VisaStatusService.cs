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
    public class VisaStatusService : CRUDService<VisaStatusEntity, VisaStatusViewModel>
    {
        public VisaStatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override VisaStatusViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new VisaStatusViewModel
            {
                Title = BshkaraRes.Skills_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<VisaStatusEntity>().Query()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Name.Ar.Contains(args.SearchString));
                        break;
                }
            }

            query.Filter(x => x.IsDeleted == false);

            query.OrderBy(q => q.OrderBy(d => d.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<VisaStatusEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(VisaStatusEntity entity)
        {
            if (UnitOfWork.Repository<MaidEntity>().Query().Filter(x => x.VisaStatusId == entity.Id).Count() > 0)
            {
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;
            }

            if (
                UnitOfWork.Repository<MaidEntity>()
                    .Query()
                    .Filter(x => x.VisaStatusId == entity.Id)
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
                    $"select name{Lang} from visastatus where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }


        public List<ApiVisaStatus> GetVisaStatusesForApi()
        {
            var skills = UnitOfWork.Context.Set<VisaStatusEntity>().Where(t => !t.IsDeleted).ToList();

            var list = skills.Select(skill => new ApiVisaStatus
            {
                Id = skill.Id,
                Name = skill.Name.Default
            }).ToList();

            return list;
        }

        public IEnumerable<IdValueModel> GetVisaStatusesIdsValues()
        {
            var countries = UnitOfWork.Context.Set<VisaStatusEntity>().Where(t => !t.IsDeleted).ToList();
            return countries.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }
    }
}