using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class PacksController : BaseCRUDController<PackageEntity, PacksViewModel, PacksService>
    {
        public PacksController(IUnitOfWork unitOfWork, PacksService service)
            : base(unitOfWork, service)
        {
        }
    }
}