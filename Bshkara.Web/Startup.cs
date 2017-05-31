using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bshkara.Web.Startup))]
namespace Bshkara.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
