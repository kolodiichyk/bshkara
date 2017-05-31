using System.Net.Http.Formatting;
using System.Web.Http;
using Bshkara.Web.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bshkara.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new RequireHttpsApiAttribute());
            config.Filters.Add(new ExecutionTimeApiFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApiActions", "api/v1/{controller}/{action}",
                new {controller = "info", action = "notfound"});

            // only json
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                CamelCaseText = true
            });
        }
    }
}