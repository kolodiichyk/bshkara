using System;
using System.Linq;
using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class MaidLanguagesController :
        BaseCRUDController
            <MaidLanguageEntity, MaidLanguagesViewModel, MaidLanguagesService>
    {
        private readonly MaidsService _maidService;

        public MaidLanguagesController(IUnitOfWork unitOfWork, MaidLanguagesService service, MaidsService maidService)
            : base(unitOfWork, service)
        {
            _maidService = maidService;
        }

        public override void PrepareForEditOrCreate(MaidLanguageEntity entity = null)
        {
            ViewBag.MaidId = entity?.MaidId ?? new Guid((string) RouteData.Values["id"]);
            ViewBag.ReturnUrl = Url.Action("Edit", "Maids", new {id = ViewBag.MaidId});
            var maid = (MaidEntity) _maidService.GetEntity(ViewBag.MaidId);

            ViewBag.LanguageId =
                new SelectList(
                    _service.GetLanguagesIdsValues(
                        maid.Languages.Where(t => t.LanguageId != entity?.LanguageId).Select(t => t.LanguageId)), "id",
                    "value",
                    entity?.LanguageId);
        }
    }
}