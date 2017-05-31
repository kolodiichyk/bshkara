using System;
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
    public class DocumentTypesService : CRUDService<DocumentTypeEntity, DocumentTypesViewModel>
    {
        public DocumentTypesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override DocumentTypesViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new DocumentTypesViewModel
            {
                Title = BshkaraRes.DocumentTypes_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<DocumentTypeEntity>().Query()
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

            viewModel.Items = new StaticPagedList<DocumentTypeEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(DocumentTypeEntity entity)
        {
            if (UnitOfWork.Repository<MaidDocumentEntity>().Query().Filter(x => x.DocumentTypeEntityId == entity.Id).Count() > 0)
            {
                return BshkaraRes.DocumentTypes_CantDeleteExistsInMaidDocuments;
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
                    $"select name{Lang} from DocumentType where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }

        public IEnumerable<IdValueModel> GetDocumentTypesIdsValues(IEnumerable<Guid> exclude = null)
        {
            exclude = exclude ?? new List<Guid>();

            var skills =
                UnitOfWork.Context.Set<DocumentTypeEntity>().Where(t => !t.IsDeleted && !exclude.Contains(t.Id)).ToList();
            return skills.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name.Default
            }).OrderBy(x => x.Value);
        }
    }
}