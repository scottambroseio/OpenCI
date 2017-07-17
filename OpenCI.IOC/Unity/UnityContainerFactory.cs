using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using OpenCI.AutoMapper;
using OpenCI.Business.Contracts;
using OpenCI.Business.Implementation;
using OpenCI.Data.Contracts;
using OpenCI.Data.Implementation;
using OpenCI.Identity.Dapper;
using OpenCI.IOC.Identity;
using ConnectionHelper = OpenCI.Data.Implementation.ConnectionHelper;
using IConnectionHelper = OpenCI.Data.Contracts.IConnectionHelper;

namespace OpenCI.IOC.Unity
{
    public static class UnityContainerFactory
    {
        public static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            // Data
            container.RegisterType<IConnectionHelper, ConnectionHelper>();
            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<IPlanData, PlanData>();

            // Business
            container.RegisterType<IProjectOperations, ProjectOperations>();
            container.RegisterType<IPlanOperations, PlanOperations>();

            // Identity
            container.RegisterType<OpenCI.Identity.Dapper.IConnectionHelper, OpenCI.Identity.Dapper.ConnectionHelper>(
                new InjectionConstructor());
            container.RegisterType<IUserStore<IdentityUser, int>, UserStore>();
            container.RegisterType<IRoleStore<IdentityRole, int>, RoleStore>();
            container.RegisterType<UserManager<IdentityUser, int>>(
                new InjectionFactory(c => UserManagerFactory.Create(c.Resolve<IUserStore<IdentityUser, int>>())));
            container.RegisterType<RoleManager<IdentityRole, int>>(
                new InjectionFactory(c => RoleManagerFactory.Create(c.Resolve<IRoleStore<IdentityRole, int>>())));
            container.RegisterType<SignInManager<IdentityUser, int>>();
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            // Misc
            container.RegisterType<IMapper>(new InjectionFactory(c => AutoMapperFactory.CreateMapper()));

            return container;
        }
    }
}