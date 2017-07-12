using Microsoft.Practices.Unity;
using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;
using OpenCI.Data.Implementation;
using OpenCI.Implementation.Business;

namespace OpenCI.IOC
{
    public static class UnityContainerFactory
    {
        public static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IConnectionHelper, ConnectionHelper>();
            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<IProjectOperations, ProjectOperations>();

            return container;
        }
    }
}
