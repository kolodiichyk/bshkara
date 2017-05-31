using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.DAL.Identity;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Extentions;
using Bshkara.Web.Models;
using Bshkara.Web.Services;
using Bshkara.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Bshkara.Web.Controllers
{
    [Authorize]
    public class AgencyUsersController :
        BaseCRUDController
            <AgencyUserEntity, AgencyUsersViewModel, AgencyUsersService>
    {
        private ApplicationUserManager _userManager;

        public AgencyUsersController(IUnitOfWork unitOfWork, AgencyUsersService service)
            : base(unitOfWork, service)
        {
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public override void PrepareForEditOrCreate(AgencyUserEntity entity = null)
        {
            ViewBag.UserId = new SelectList(_service.GetUsersIdsValues(), "id", "value", entity?.UserId);
            ViewBag.AgencyId = User.Identity.GetAgencyId();
        }

        public ActionResult RegisterAgencyUser()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAgencyUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity {UserName = model.Email, Email = model.Email, Name = model.Email};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, RoleEntity.AgentUserRoleName);

                    var guid = User.Identity.GetAgencyId();
                    if (guid != null)
                    {
                        var agencyId = guid.Value;
                        //Set agency to user
                        var agencyUsersService = DependencyResolver.Current.GetService<AgencyUsersService>();
                        agencyUsersService.InsertOrUpdate(new AgencyUserEntity
                        {
                            AgencyId = agencyId,
                            UserId = user.Id
                        });
                    }

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}