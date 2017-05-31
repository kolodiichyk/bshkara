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
    public class AgencyPacksController :
        BaseCRUDController
            <AgencyPackageEntity, AgencyPacksViewModel, AgencyPacksService>
    {
        public AgencyPacksController(IUnitOfWork unitOfWork, AgencyPacksService service)
            : base(unitOfWork, service)
        {
        }

        public override void PrepareForEditOrCreate(AgencyPackageEntity entity = null)
        {
            ViewBag.PackageId = new SelectList(_service.GetPackageIdsValues(), "id", "value", entity?.PackageId);

            if (entity == null && RouteData.Values.ContainsKey("id"))
            {
                ViewBag.AgencyId = new Guid((string) RouteData.Values["id"]);
            }
        }
    }
}