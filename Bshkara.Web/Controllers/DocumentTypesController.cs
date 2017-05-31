using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class DocumentTypesController : BaseCRUDController<DocumentTypeEntity, DocumentTypesViewModel, DocumentTypesService>
    {
        public DocumentTypesController(IUnitOfWork unitOfWork, DocumentTypesService service)
            : base(unitOfWork, service)
        {
        }
    }
}