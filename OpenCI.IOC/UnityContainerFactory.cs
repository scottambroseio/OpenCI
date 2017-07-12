using Microsoft.Practices.Unity;
using OpenCI.Contracts.Data;
using OpenCI.Implementation.Data;

namespace OpenCI.IOC
{
    public static class UnityContainerFactory
    {
        public static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IConnectionHelper, ConnectionHelper>();
            container.RegisterType<IProjectData, ProjectData>();

            return container;
        }
    }
}
