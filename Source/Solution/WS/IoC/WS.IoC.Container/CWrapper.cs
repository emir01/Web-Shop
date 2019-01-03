using StructureMap;
using WS.IoC.Config.Boot;

namespace WS.IoC.Container
{
    public static class CWrapper
    {
        #region Container

        private static IContainer _container;

        #endregion

        public static IContainer Container => _container ?? (_container = IoCInitializer.Initialize());
    }
}
