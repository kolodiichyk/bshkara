using System.Web.Http;
using System.Web.Mvc;
using Bshkara.Web.Areas.ApiHelp.App_Start;
using WebApplication2.Areas.HelpPage;

namespace Bshkara.Web.Areas.ApiHelp
{
    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ApiHelp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ApiHelp_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}