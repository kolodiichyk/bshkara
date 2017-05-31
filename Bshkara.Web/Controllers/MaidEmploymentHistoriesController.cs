using System;
using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class MaidEmploymentHistoriesController :
        BaseCRUDController
            <MaidEmploymentHistoryEntity, MaidEmploymentHistoriesViewModel, MaidEmploymentHistoriesService>
    {
        private readonly CountriesService _countriesService;

        public MaidEmploymentHistoriesController(IUnitOfWork unitOfWork, MaidEmploymentHistoriesService service,
            CountriesService countriesService)
            : base(unitOfWork, service)
        {
            _countriesService = countriesService;
        }

        public override void PrepareForEditOrCreate(MaidEmploymentHistoryEntity entity = null)
        {
            ViewBag.CountryId = new SelectList(
                _countriesService.GetCountriesIdsValues(), "id", "value", entity?.CountryId);

            ViewBag.MaidId = entity?.MaidId ?? new Guid((string) RouteData.Values["id"]);
            ViewBag.ReturnUrl = Url.Action("Edit", "Maids", new {id = ViewBag.MaidId});
        }
    }
}