using System.Web.Http;
using Microsoft.Owin;
using OpenCI.API.Rest;
using OpenCI.API.Rest.Config;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OpenCI.API.Rest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            WebApiConfig.Register(httpConfiguration);
            RouteConfig.Register(httpConfiguration.Routes);
            IdentityConfig.Register(app);

            app.UseWebApi(httpConfiguration);
        }
    }
}