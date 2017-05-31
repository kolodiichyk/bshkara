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
    public class CitiesController : BaseCRUDController<CityEntity, CitiesViewModel, CitiesService>
    {
        private readonly CountriesService _countriesService;

        public CitiesController(IUnitOfWork unitOfWork, CitiesService service, CountriesService countriesService)
            : base(unitOfWork, service)
        {
            _countriesService = countriesService;
        }

        public override void PrepareForEditOrCreate(CityEntity entity = null)
        {
            base.PrepareForEditOrCreate(entity);

            ViewBag.CountryId = new SelectList(
                _countriesService.GetCountriesIdsValues(), "id", "value", entity?.CountryId);
        }
    }
}