using Microsoft.Owin;
using Owin;
using System.Web.Http;
using OpenCI.API.Rest.Config;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

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

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseWebApi(httpConfiguration);
        }
    }
}
