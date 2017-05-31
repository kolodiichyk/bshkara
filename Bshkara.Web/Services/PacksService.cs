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
    public class PacksService : CRUDService<PackageEntity, PacksViewModel>
    {
        public PacksService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override PacksViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new PacksViewModel
            {
                Title = BshkaraRes.Skills_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<PackageEntity>().Query()
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

            query.OrderBy(q => q.OrderBy(d => d.Price));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<PackageEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(PackageEntity entity)
        {
            if (UnitOfWork.Repository<AgencyPackageEntity>().Query().Filter(x => x.PackageId == entity.Id).Count() > 0)
            {
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;
            }

            if (
                UnitOfWork.Repository<AgencyPackageEntity>()
                    .Query()
                    .Filter(x => x.PackageId == entity.Id)
                    .Count() > 0)
            {
                return BshkaraRes.Packages_CantDeleteExistsInAgencyPackage;
            }

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                    $"select name{Lang} from packages where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }
    }
}