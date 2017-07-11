using OpenCI.IOC;
using System.Net.Http.Headers;
using System.Web.Http;

namespace OpenCI.API.Rest.Config
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "home",
                routeTemplate: "home",
                defaults: new { controller = "home", action = "Get" }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.DependencyResolver = UnityContainerFactory.CreateResolver();
        }
    }
}