using Microsoft.Practices.Unity;

namespace OpenCI.IOC
{
    public static class UnityContainerFactory
    {
        public static UnityResolver CreateResolver()
        {
            var container = CreateContainer();

            var dependancyResolver = new UnityResolver(container);

            return dependancyResolver;
        }

        private static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}
