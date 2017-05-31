using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class PassportsController :
        BaseCRUDController<MaidPassportDetailEntity, PassportsViewModel, PassportsService>
    {
        public PassportsController(IUnitOfWork unitOfWork, PassportsService service)
            : base(unitOfWork, service)
        {
        }
    }
}