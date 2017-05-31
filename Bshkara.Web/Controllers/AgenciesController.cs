using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Models;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class AgenciesController : BaseCRUDController<AgencyEntity, AgenciesViewModel, AgenciesService>
    {
        private readonly AgencyPacksService _agencyPackagesService;
        private readonly AgencyUsersService _agencyUsersService;
        private readonly CitiesService _citiesService;
        private readonly CountriesService _countriesService;

        public AgenciesController(IUnitOfWork unitOfWork, AgenciesService service, CountriesService countriesService,
            AgencyUsersService agencyUsersService, AgencyPacksService agencyPackagesService, CitiesService citiesService)
            : base(unitOfWork, service)
        {
            _countriesService = countriesService;
            _agencyUsersService = agencyUsersService;
            _agencyPackagesService = agencyPackagesService;
            _citiesService = citiesService;
        }

        public override void PrepareForEditOrCreate(AgencyEntity entity = null)
        {
            base.PrepareForEditOrCreate(entity);

            ViewBag.CityId = new SelectList(
                _citiesService.GetCitiesIdsValues(), "id", "value", entity?.CityId);

            if (entity == null)
                return;

            ViewBag.AgencyUsersViewModel =
                _agencyUsersService.GetViewModelForIndex(new FilterArgs {AgencyId = entity.Id});

            ViewBag.AgencyPackagesViewModel =
                _agencyPackagesService.GetViewModelForIndex(new FilterArgs {AgencyId = entity.Id});

            ViewBag.ReturnUrl = User.IsInRole(RoleEntity.AgentAdminRoleName)
                ? Url.Action("Edit", "Agencies", new {id = entity.Id})
                : "/Agenicies/Index";
        }
    }
}