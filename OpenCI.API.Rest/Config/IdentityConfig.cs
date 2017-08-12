using System;
using System.Configuration;
using System.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OpenCI.Identity.Dapper;
using OpenCI.Identity.ExtensionPoints.Services;
using Owin;

namespace OpenCI.API.Rest.Config
{
    public static class IdentityConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateIdentityDbConnection);
            app.CreatePerOwinContext<UserStore>(CreateUserStore);
            app.CreatePerOwinContext<RoleStore>(CreateRoleStore);
            app.CreatePerOwinContext<UserManager<IdentityUser, int>>(CreateUserManager);
            app.CreatePerOwinContext<RoleManager<IdentityRole, int>>(CreateRoleManager);
            app.CreatePerOwinContext<SignInManager<IdentityUser, int>>(
                (opt, ctx) => new SignInManager<IdentityUser, int>(ctx.Get<UserManager<IdentityUser, int>>(),
                    ctx.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }

        private static IDbConnection CreateIdentityDbConnection()
        {
            return new ConnectionHelper().Connection;
        }

        private static UserStore CreateUserStore(
            IdentityFactoryOptions<UserStore> opts, IOwinContext ctx)
        {
            return new UserStore(ctx.Get<IDbConnection>());
        }

        private static RoleStore CreateRoleStore(
            IdentityFactoryOptions<RoleStore> opts, IOwinContext ctx)
        {
            return new RoleStore(ctx.Get<IDbConnection>());
        }

        private static RoleManager<IdentityRole, int> CreateRoleManager(
            IdentityFactoryOptions<RoleManager<IdentityRole, int>> opts, IOwinContext ctx)
        {
            var roleStore = ctx.Get<RoleStore>();
            var roleManager = new RoleManager<IdentityRole, int>(roleStore);

            roleManager.RoleValidator = new RoleValidator<IdentityRole, int>(roleManager);

            return roleManager;
        }

        private static UserManager<IdentityUser, int> CreateUserManager(
            IdentityFactoryOptions<UserManager<IdentityUser, int>> opts, IOwinContext ctx)
        {
            var userStore = ctx.Get<UserStore>();

            var userManager = new UserManager<IdentityUser, int>(userStore)
            {
                UserTokenProvider =
                    new DataProtectorTokenProvider<IdentityUser, int>(
                        opts.DataProtectionProvider.Create("ASP.NET Identity")),
                EmailService = new IdentityEmailService(),
                UserLockoutEnabledByDefault = bool.Parse(ConfigurationManager.AppSettings["lockout:enabled"]),
                DefaultAccountLockoutTimeSpan =
                    TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["lockout:timespan"])),
                MaxFailedAccessAttemptsBeforeLockout = int.Parse(ConfigurationManager.AppSettings["lockout:attempts"])
            };

            userManager.UserValidator = new UserValidator<IdentityUser, int>(userManager)
            {
                RequireUniqueEmail = true
            };

            return userManager;
        }
    }
}