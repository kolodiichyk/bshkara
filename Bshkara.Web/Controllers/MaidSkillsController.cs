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
    public class MaidSkillsController :
        BaseCRUDController
            <MaidSkillEntity, MaidSkillsViewModel, MaidSkillsService>
    {
        private readonly MaidsService _maidService;

        public MaidSkillsController(IUnitOfWork unitOfWork, MaidSkillsService service, MaidsService maidService)
            : base(unitOfWork, service)
        {
            _maidService = maidService;
        }

        public override void PrepareForEditOrCreate(MaidSkillEntity entity = null)
        {
            ViewBag.MaidId = entity?.MaidId ?? new Guid((string) RouteData.Values["id"]);
            ViewBag.ReturnUrl = Url.Action("Edit", "Maids", new {id = ViewBag.MaidId});
            var maid = (MaidEntity) _maidService.GetEntity(ViewBag.MaidId);

            ViewBag.SkillId =
                new SelectList(
                    _service.GetSkillsIdsValues(
                        maid.Skills.Where(t => t.SkillId != entity?.SkillId).Select(t => t.SkillId)),
                    "id",
                    "value",
                    entity?.SkillId);
        }
    }
}