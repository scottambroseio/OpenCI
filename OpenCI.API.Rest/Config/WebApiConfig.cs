using OpenCI.IOC;
using System.Net.Http.Headers;
using System.Web.Http;

namespace OpenCI.API.Rest.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.DependencyResolver = UnityResolverFactory.CreateResolver();
        }
    }
}