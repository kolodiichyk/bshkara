using System.Reflection;
using Autofac;

namespace Bshkara.Web.Extentions
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register services
        /// </summary>
        public static ContainerBuilder RegisterServices(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => x.Name.EndsWith("Service"));

            return builder;
        }
    }
}