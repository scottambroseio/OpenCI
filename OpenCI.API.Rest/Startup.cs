using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseWebApi(httpConfiguration);
        }
    }
}