using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Extentions;
using Bshkara.Web.Models;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class MaidsController : BaseCRUDController<MaidEntity, MaidsViewModel, MaidsService>
    {
        private readonly AgenciesService _agenciesService;
        private readonly CitiesService _citiesService;
        private readonly MaidDocumentsService _maidDocumentsService;
        private readonly MaidEmploymentHistoriesService _maidEmploymentHistoriesService;
        private readonly MaidLanguagesService _maidLanguagesService;
        private readonly MaidSkillsService _maidSkillsService;
        private readonly NationalitiesService _nationalitiesService;
        private readonly VisaStatusService _visaStatusService;
        private readonly AgencyPacksService _agencyPacksService;

        public MaidsController(IUnitOfWork unitOfWork, MaidsService service,
            MaidEmploymentHistoriesService maidEmploymentHistoriesService, MaidLanguagesService maidLanguagesService,
            MaidSkillsService maidSkillsService, MaidDocumentsService maidDocumentsService,
            AgenciesService agenciesService, CitiesService citiesService, NationalitiesService nationalitiesService, 
            VisaStatusService visaStatusService, AgencyPacksService agencyPacksService)
            : base(unitOfWork, service)
        {
            _maidEmploymentHistoriesService = maidEmploymentHistoriesService;
            _maidLanguagesService = maidLanguagesService;
            _maidSkillsService = maidSkillsService;
            _maidDocumentsService = maidDocumentsService;
            _agenciesService = agenciesService;
            _citiesService = citiesService;
            _nationalitiesService = nationalitiesService;
            _visaStatusService = visaStatusService;
            _agencyPacksService = agencyPacksService;
        }

        public override void PrepareForEditOrCreate(MaidEntity entity = null)
        {
            base.PrepareForEditOrCreate(entity);

            if (!User.IsInRole(RoleEntity.AdminRoleName))
                ViewBag.AgencyId = User.Identity.GetAgency()?.Id;
            else
                ViewBag.AgencyId = new SelectList(
                    _agenciesService.GetAgenciesIdsValues(), "id", "value", entity?.AgencyId);

            ViewBag.LivingCityId = new SelectList(
                _citiesService.GetCitiesIdsValues(), "id", "value", entity?.LivingCityId);

            ViewBag.NationalityId = new SelectList(
                _nationalitiesService.GetNationalitiesIdsValues(), "id", "value", entity?.NationalityId);

            ViewBag.VisaStatusId = new SelectList(
            _visaStatusService.GetVisaStatusesIdsValues(), "id", "value", entity?.VisaStatusId);

            ViewBag.AgencyPackageId = new SelectList(
            _agencyPacksService.GetAgencyPackagesIdsValues(entity.AgencyId ?? User.Identity.GetAgency()?.Id), "id", "value", entity?.AgencyPackageId);

            if (entity == null)
                return;

            ViewBag.ReturnUrl = Url.Action("Edit", "Maids", new {id = entity.Id});
            ViewBag.Passport = entity.Passport;
            ViewBag.MaidEmploymentHistoriesViewModel =
                _maidEmploymentHistoriesService.GetViewModelForIndex(new FilterArgs {MaidId = entity.Id});
            ViewBag.MaidLanguagesViewModel =
                _maidLanguagesService.GetViewModelForIndex(new FilterArgs {MaidId = entity.Id});
            ViewBag.MaidSkillsViewModel = _maidSkillsService.GetViewModelForIndex(new FilterArgs {MaidId = entity.Id});
            ViewBag.MaidDocumentsViewModel =
                _maidDocumentsService.GetViewModelForIndex(new FilterArgs {MaidId = entity.Id});
        }
    }
}