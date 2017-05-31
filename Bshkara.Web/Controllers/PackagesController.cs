using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class PackagesController : BaseCRUDController<PackageEntity, PackagesViewModel, PackagesService>
    {
        public PackagesController(IUnitOfWork unitOfWork, PackagesService service)
            : base(unitOfWork, service)
        {
        }
    }
}