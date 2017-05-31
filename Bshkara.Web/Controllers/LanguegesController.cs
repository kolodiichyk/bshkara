using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class LanguagesController : BaseCRUDController<LanguageEntity, LanguagesViewModel, LanguagesService>
    {
        public LanguagesController(IUnitOfWork unitOfWork, LanguagesService service)
            : base(unitOfWork, service)
        {
        }
    }
}