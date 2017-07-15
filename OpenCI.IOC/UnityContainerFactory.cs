using Microsoft.Practices.Unity;
using OpenCI.AutoMapper;
using OpenCI.Business.Contracts;
using OpenCI.Business.Implementation;
using OpenCI.Data.Contracts;
using OpenCI.Data.Implementation;

namespace OpenCI.IOC
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

            // Misc
            container.RegisterInstance(AutoMapperFactory.CreateMapper());

            return container;
        }
    }
}
