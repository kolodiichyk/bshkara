using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Bashkra.Shared.Enums;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class AgencyPacksService :
        CRUDService<AgencyPackageEntity, AgencyPacksViewModel>
    {
        public AgencyPacksService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override AgencyPacksViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new AgencyPacksViewModel
            {
                Title = BshkaraRes.MaidLanguages_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize,
                AgencyId = args.AgencyId
            };

            var query = UnitOfWork.Repository<AgencyPackageEntity>().Query()
                .Include(x => x.Package)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
            {
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Package.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Package.Name.Ar.Contains(args.SearchString));
                        break;
                }
            }

            query.Filter(x => x.IsDeleted == false && args.AgencyId == null || x.AgencyId == args.AgencyId);
            query.OrderBy(q => q.OrderBy(d => d.Package.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<AgencyPackageEntity>(items, args.PageNumber, args.PageSize,
                count);

            return viewModel;
        }

        public override string CanDeleteEntity(AgencyPackageEntity entity)
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

        public IEnumerable<IdValueModel> GetPackageIdsValues()
        {
            var packages =
                UnitOfWork.Context.Set<PackageEntity>().Where(t => !t.IsDeleted).OrderBy(t => t.Price).ToList();
            return packages.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = $"{x.Name.En} ({x.Price}$)"
            });
        }

        public IEnumerable<IdValueModel> GetAgencyPackagesIdsValues(Guid? AgencyId)
        {

            Expression<Func<AgencyPackageEntity, bool>> agencyPredicate = agencyPackage => true;

            if (AgencyId != null)
            {
                agencyPredicate = agencyPackage => agencyPackage.AgencyId == AgencyId.Value;
            }

            var agencyPackages =
                UnitOfWork.Context.Set<AgencyPackageEntity>().Where(t => !t.IsDeleted && t.PackageStatus == PackageStatus.Active).Where(agencyPredicate).Include(t=>t.Package).ToList();
            return agencyPackages.Select(x => new IdValueModel
            {
                Id = x.Id,
                Value = $"{x.Package.Name.Default} (left { x.Package.Duration - (DateTime.UtcNow - x.CreatedAt.Value).Days } days)"
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