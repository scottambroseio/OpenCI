using AutoMapper;
using Microsoft.Practices.Unity;
using OpenCI.AutoMapper;
using OpenCI.Business.Contracts;
using OpenCI.Business.Implementation;
using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;
using OpenCI.Data.Implementation;
using OpenCI.Implementation.Business;

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
            container.RegisterType<IEmailRenderService, EmailRenderService>();

            // Misc
            container.RegisterType<IMapper>(new InjectionFactory(c => AutoMapperFactory.CreateMapper()));

            return container;
        }
    }
}