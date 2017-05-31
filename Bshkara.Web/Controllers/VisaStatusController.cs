using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class VisaStatusController : BaseCRUDController<VisaStatusEntity, VisaStatusViewModel, VisaStatusService>
    {
        public VisaStatusController(IUnitOfWork unitOfWork, VisaStatusService service)
            : base(unitOfWork, service)
        {
        }
    }
}