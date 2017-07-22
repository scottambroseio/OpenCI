using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OpenCI.Identity.Dapper;
using Owin;

namespace OpenCI.API.Rest.Config
{
    public static class IdentityConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserStore<IdentityUser, int>>(() => new UserStore(new ConnectionHelper()));
            app.CreatePerOwinContext<IRoleStore<IdentityRole, int>>(() => new RoleStore(new ConnectionHelper()));
            app.CreatePerOwinContext<UserManager<IdentityUser, int>>(CreateUserManager);
            app.CreatePerOwinContext<RoleManager<IdentityRole, int>>(CreateRoleManager);
            app.CreatePerOwinContext<SignInManager<IdentityUser, int>>((opt, ctx) => new SignInManager<IdentityUser, int>(ctx.Get<UserManager<IdentityUser, int>>(), ctx.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }

        private static RoleManager<IdentityRole, int> CreateRoleManager(IdentityFactoryOptions<RoleManager<IdentityRole, int>> opts, IOwinContext ctx)
        {
            var roleStore = ctx.Get<IRoleStore<IdentityRole, int>>();
            var roleManager = new RoleManager<IdentityRole, int>(roleStore);

            return roleManager;
        }

        private static UserManager<IdentityUser, int> CreateUserManager(IdentityFactoryOptions<UserManager<IdentityUser, int>> opts, IOwinContext ctx)
        {
            var userStore = ctx.Get<IUserStore<IdentityUser, int>>();
            var userManager = new UserManager<IdentityUser, int>(userStore)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<IdentityUser, int>(
                        opts.DataProtectionProvider.Create("ASP.NET Identity"))
            };


            return userManager;
        }
    }
}