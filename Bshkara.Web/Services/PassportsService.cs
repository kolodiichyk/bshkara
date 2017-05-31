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
    public class PassportsService : CRUDService<MaidPassportDetailEntity, PassportsViewModel>
    {
        public PassportsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override PassportsViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new PassportsViewModel
            {
                Title = BshkaraRes.Nationalities_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<MaidPassportDetailEntity>().Query()
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                query.Filter(x => x.PassportNumber.Contains(args.SearchString));
            }

            query.Filter(x => x.IsDeleted == false);
            query.OrderBy(q => q.OrderBy(d => d.PassportNumber));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<MaidPassportDetailEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(MaidPassportDetailEntity entity)
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
                    $"select name{Lang} from maidpassportdetails where isDeleted = 0 and passportnumber like N'%{key}%' order by passportnumber")
                    .ToList();
        }
    }
}