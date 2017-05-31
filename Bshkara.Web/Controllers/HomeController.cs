using System.Web.Mvc;
using Bshkara.Web.Controllers.Bases;
using Bshkara.Web.Models;
using Bshkara.Web.Services;

namespace Bshkara.Web.Controllers
{
    public class HomeController : BaseHttpsController
    {
        private readonly PacksService _packsService;

        public HomeController(PacksService packsService)
        {
            _packsService = packsService;
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Maids");
            }

            var packsViewModel = _packsService.GetViewModelForIndex(new FilterArgs());
            return View(packsViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}