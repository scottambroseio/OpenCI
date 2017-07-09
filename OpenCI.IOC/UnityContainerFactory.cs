using Microsoft.Practices.Unity;

namespace OpenCI.IOC
{
    public static class UnityContainerFactory
    {
        public static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}
