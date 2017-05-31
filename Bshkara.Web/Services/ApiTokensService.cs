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
    public class ApiTokensService : CRUDService<ApiTokenEntity, ApiTokensViewModel>
    {
        public ApiTokensService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override ApiTokensViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new ApiTokensViewModel
            {
                Title = "API keys",
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<ApiTokenEntity>().Query()
                .Include(x => x.User)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                query.Filter(x => x.Name.Contains(args.SearchString));
            }

            query.Filter(x => x.IsDeleted == false);

            query.OrderBy(q => q.OrderBy(x => x.Name));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<ApiTokenEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(ApiTokenEntity entity)
        {
            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string term)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                    $"select name from apitokens where isDeleted = 0 and name like N'%{term}%' order by name").ToList();
        }

        public ApiTokenEntity GetTokenByKey(string key)
        {
            return UnitOfWork.Repository<ApiTokenEntity>().Query().Filter(x => x.Token == key).Get().FirstOrDefault();
        }
    }
}