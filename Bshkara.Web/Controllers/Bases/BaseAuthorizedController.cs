using System.Web.Mvc;

namespace Bshkara.Web.Controllers.Bases
{
    [System.Web.Http.Authorize]
    public class BaseAuthorizedController : BaseHttpsController
    {
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}