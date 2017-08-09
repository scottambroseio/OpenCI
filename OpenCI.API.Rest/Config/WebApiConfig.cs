using System.Net.Http.Headers;
using System.Web.Http;
using OpenCI.API.Rest.Attributes;
using OpenCI.IOC;
using OpenCI.IOC.Unity;

namespace OpenCI.API.Rest.Config
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.DependencyResolver = UnityResolverFactory.CreateResolver();
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}