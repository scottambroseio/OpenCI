using Microsoft.Owin;
using Owin;
using System.Web.Http;
using OpenCI.API.Rest.Config;

[assembly: OwinStartup(typeof(OpenCI.API.Rest.Startup))]

namespace OpenCI.API.Rest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();

            WebApiConfig.Register(httpConfiguration);
            RouteConfig.Register(httpConfiguration.Routes);

            app.UseWebApi(httpConfiguration);
        }
    }
}
