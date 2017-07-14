using System.Web.Http;

namespace OpenCI.API.Rest.Config
{
    public static class RouteConfig
    {
        public static void Register(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "project",
                routeTemplate: "project/{guid}",
                defaults: new { controller = "project" }
            );
        }
    }
}