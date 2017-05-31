using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class CountriesController : BaseCRUDController<CountryEntity, CountriesViewModel, CountriesService>
    {
        public CountriesController(IUnitOfWork unitOfWork, CountriesService service)
            : base(unitOfWork, service)
        {
        }
    }
}