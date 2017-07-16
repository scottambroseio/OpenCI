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
using System.Web;

namespace OpenCI.IOC
{
    public static class UnityContainerFactory
    {
        public static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            // Data
            container.RegisterType<Data.Contracts.IConnectionHelper, Data.Implementation.ConnectionHelper>();
            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<IPlanData, PlanData>();

            // Business
            container.RegisterType<IProjectOperations, ProjectOperations>();
            container.RegisterType<IPlanOperations, PlanOperations>();

            // Identity
            container.RegisterType<Identity.Dapper.IConnectionHelper, Identity.Dapper.ConnectionHelper>(new InjectionConstructor());
            container.RegisterType<IUserStore<IdentityUser, int>, UserStore>();
            container.RegisterType<IRoleStore<IdentityRole, int>, RoleStore>();
            container.RegisterType<UserManager<IdentityUser, int>>();
            container.RegisterType<RoleManager<IdentityRole, int>>();
            container.RegisterType<SignInManager<IdentityUser, int>>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            // Misc
            container.RegisterType<IMapper>(new InjectionFactory(c => AutoMapperFactory.CreateMapper()));

            return container;
        }
    }
}
