using System;
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
    public class MaidLanguagesService :
        CRUDService<MaidLanguageEntity, MaidLanguagesViewModel>
    {
        public MaidLanguagesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override MaidLanguagesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new MaidLanguagesViewModel
            {
                Title = BshkaraRes.MaidLanguages_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize,
                MaidId = args.MaidId
            };

            var query = UnitOfWork.Repository<MaidLanguageEntity>().Query()
                .Include(x => x.Language)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Language.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Language.Name.Ar.Contains(args.SearchString));
                        break;
                }
            }

            query.Filter(x => x.IsDeleted == false && (args.MaidId == null || x.MaidId == args.MaidId));
            query.OrderBy(q => q.OrderBy(d => d.Language.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<MaidLanguageEntity>(items, args.PageNumber, args.PageSize,
                count);

            return viewModel;
        }

        public override string CanDeleteEntity(MaidLanguageEntity entity)
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

        public IEnumerable<IdValueModel> GetLanguagesIdsValues(IEnumerable<Guid> exception)
        {
            var languages =
                UnitOfWork.Context.Set<LanguageEntity>().Where(t => !t.IsDeleted && !exception.Contains(t.Id)).ToList();
            return languages.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                    $"select description{Lang} from MaidLanguages where isDeleted = 0 and description{Lang} like N'%{key}%' order by description{Lang}")
                    .ToList();
        }
    }
}