using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Bshkara.Core.Services;
using Bshkara.DAL.DB;
using Bshkara.Web.Extentions;

namespace Bshkara.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterType<EFDbContext>().As<IDbContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterServices(typeof (MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}