using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Extentions;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class AgencyUsersService :
        CRUDService<AgencyUserEntity, AgencyUsersViewModel>
    {
        public AgencyUsersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override AgencyUsersViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new AgencyUsersViewModel
            {
                Title = BshkaraRes.MaidLanguages_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize,
                AgencyId = args.AgencyId
            };

            var query = UnitOfWork.Repository<AgencyUserEntity>().Query()
                .Include(x => x.User)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                query.Filter(x => x.User.Name.Contains(args.SearchString));
            }

            var agency = HttpContext.Current.User.Identity.GetAgency();
            if (agency != null)
            {
                query.Filter(x => x.AgencyId == agency.Id);
            }

            query.Filter(x => x.IsDeleted == false);
            query.OrderBy(q => q.OrderBy(d => d.User.Name));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<AgencyUserEntity>(items, args.PageNumber, args.PageSize,
                count);

            return viewModel;
        }

        public override string CanDeleteEntity(AgencyUserEntity entity)
        {
            if (
                UnitOfWork.Repository<AgencyEntity>()
                    .Query()
                    .Filter(x => x.Id == entity.AgencyId)
                    .Count() > 0)
            {
                return BshkaraRes.Countries_CantDeleteExistsInMaidEmploymentHistory;
            }

            return string.Empty;
        }

        public IEnumerable<IdValueModel> GetUsersIdsValues()
        {
            var users = UnitOfWork.Context.Set<UserEntity>().Where(t => !t.IsDeleted).ToList();
            return users.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = x.Name
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