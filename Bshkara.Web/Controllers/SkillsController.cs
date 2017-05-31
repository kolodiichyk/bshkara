using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class SkillsController : BaseCRUDController<SkillEntity, SkillsViewModel, SkillsService>
    {
        public SkillsController(IUnitOfWork unitOfWork, SkillsService service)
            : base(unitOfWork, service)
        {
        }
    }
}