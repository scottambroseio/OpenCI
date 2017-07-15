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

            container.RegisterType<IConnectionHelper, ConnectionHelper>();
            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<IPlanData, PlanData>();
            container.RegisterType<IProjectOperations, ProjectOperations>();
            container.RegisterInstance(AutoMapperFactory.CreateMapper());

            return container;
        }
    }
}
