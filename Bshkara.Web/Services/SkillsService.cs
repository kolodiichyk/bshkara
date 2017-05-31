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
    public class SkillsService : CRUDService<SkillEntity, SkillsViewModel>
    {
        public SkillsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override SkillsViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new SkillsViewModel
            {
                Title = BshkaraRes.Skills_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<SkillEntity>().Query()
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

            viewModel.Items = new StaticPagedList<SkillEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(SkillEntity entity)
        {
            if (UnitOfWork.Repository<MaidSkillEntity>().Query().Filter(x => x.SkillId == entity.Id).Count() > 0)
            {
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;
            }

            if (
                UnitOfWork.Repository<MaidSkillEntity>()
                    .Query()
                    .Filter(x => x.SkillId == entity.Id)
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
                    $"select name{Lang} from skills where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }


        public List<ApiSkill> GetSkillsForApi()
        {
            var skills = UnitOfWork.Context.Set<SkillEntity>().Where(t => !t.IsDeleted).ToList();

            var list = skills.Select(skill => new ApiSkill
            {
                Id = skill.Id,
                Name = skill.Name.Default,
                Icon = skill.Icon
            }).ToList();

            return list;
        }
    }
}