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
    public class LanguagesService : CRUDService<LanguageEntity, LanguagesViewModel>
    {
        public LanguagesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override LanguagesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new LanguagesViewModel
            {
                Title = BshkaraRes.Languages_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<LanguageEntity>().Query()
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

            viewModel.Items = new StaticPagedList<LanguageEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(LanguageEntity entity)
        {
            if (UnitOfWork.Repository<MaidLanguageEntity>().Query().Filter(x => x.LanguageId == entity.Id).Count() > 0)
            {
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;
            }

            if (
                UnitOfWork.Repository<MaidLanguageEntity>()
                    .Query()
                    .Filter(x => x.LanguageId == entity.Id)
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
                    $"select name{Lang} from languages where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }

        public List<ApiLanguage> GetLanguagesForApi()
        {
            var languages = UnitOfWork.Context.Set<LanguageEntity>().Where(t => !t.IsDeleted).ToList();

            var list = languages.Select(language => new ApiLanguage
            {
                Id = language.Id,
                ShortName = language.ShortName,
                Name = language.Name.Default
            }).ToList();

            return list;
        }
    }
}