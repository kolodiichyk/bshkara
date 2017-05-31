using System;
using System.Collections.Generic;
using System.Linq;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class MaidDocumentsService :
        CRUDService<MaidDocumentEntity, MaidDocumentsViewModel>
    {
        public MaidDocumentsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override MaidDocumentsViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new MaidDocumentsViewModel
            {
                Title = BshkaraRes.MaidDocuments_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize,
                MaidId = args.MaidId
            };

            var query = UnitOfWork.Repository<MaidDocumentEntity>().Query()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                query.Filter(x => x.FileName.Contains(args.SearchString));
            }

            query.Filter(x => x.IsDeleted == false && (args.MaidId == Guid.Empty || x.MaidId == args.MaidId));
            query.OrderBy(q => q.OrderBy(d => d.FileName));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<MaidDocumentEntity>(items, args.PageNumber, args.PageSize,
                count);

            return viewModel;
        }

        public override string CanDeleteEntity(MaidDocumentEntity entity)
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
                    $"select description{Lang} from MaidDocuments where isDeleted = 0 and description{Lang} like N'%{key}%' order by description{Lang}")
                    .ToList();
        }
    }
}