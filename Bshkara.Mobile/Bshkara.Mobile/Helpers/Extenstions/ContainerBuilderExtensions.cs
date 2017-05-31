using System.Reflection;
using Autofac;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace Bshkara.Mobile.Helpers.Extenstions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterMvvmComponents(this ContainerBuilder builder,
            params Assembly[] assemblies)
        {
            builder
                .RegisterViewModels(assemblies)
                .RegisterViews(assemblies);

            return builder;
        }

        public static ContainerBuilder RegisterViewModels(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x =>
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().IsSubclassOf(typeof (ViewModel))
                )
                .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder RegisterViews(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x =>
                    x.GetTypeInfo().IsClass &&
                    !x.GetTypeInfo().IsAbstract &&
                    x.GetTypeInfo().IsSubclassOf(typeof (ContentPage))
                )
                .InstancePerDependency();

            return builder;
        }

        public static ContainerBuilder RegisterServices(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces();

            return builder;
        }
    }
}