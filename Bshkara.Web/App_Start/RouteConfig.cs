using System.Web.Mvc;
using System.Web.Routing;

namespace Bshkara.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Profile",
                "Profile/{id}",
                new {controller = "Agencies", action = "Edit", id = ""}
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}