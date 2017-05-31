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
    public class MaidDocumentsController :
        BaseCRUDController
            <MaidDocumentEntity, MaidDocumentsViewModel, MaidDocumentsService>
    {
        private DocumentTypesService _documentTypesService;

        public MaidDocumentsController(IUnitOfWork unitOfWork, MaidDocumentsService service, DocumentTypesService documentTypesService)
            : base(unitOfWork, service)
        {
            _documentTypesService = documentTypesService;
        }

        public override void PrepareForEditOrCreate(MaidDocumentEntity entity = null)
        {
            ViewBag.MaidId = entity?.MaidId ?? new Guid((string) RouteData.Values["id"]);
            ViewBag.ReturnUrl = Url.Action("Edit", "Maids", new {id = ViewBag.MaidId});

            ViewBag.DocumentTypeEntityId = new SelectList(
            _documentTypesService.GetDocumentTypesIdsValues(), "id", "value", entity?.DocumentTypeEntityId);
        }
    }
}