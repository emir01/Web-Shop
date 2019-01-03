using System.Configuration;
using StructureMap.Pipeline;

namespace WS.IoC.Config
{
    public class BootConfigParameters
    {
        private string unitOfWorkSettingsKey = "UnitOfWorkLifecycle";
        private readonly ILifecycle _unitOfWorkDefaultLifecycle = Lifecycles.Transient;
        
        private string dbContextSettingsKey = "UnitOfWorkLifecycle";
        private readonly ILifecycle _dbContextDefaultLifecycle = Lifecycles.Transient;

        public ILifecycle GetUnitOfWorkLifecycle()
        {
            var lifecycle = ConfigurationManager.AppSettings[unitOfWorkSettingsKey];

            if (string.IsNullOrWhiteSpace(lifecycle))
            {
                return _unitOfWorkDefaultLifecycle;
            }
            else
            {
                var configuredCycle = GetLifecycleForConfig(lifecycle);
                if (configuredCycle == null)
                {
                    return _unitOfWorkDefaultLifecycle;
                }
                else
                {
                    return configuredCycle;
                }
            }
        }

        public ILifecycle GetDbContextLifecycle()
        {
            var lifecycle = ConfigurationManager.AppSettings[dbContextSettingsKey];

            if (string.IsNullOrWhiteSpace(lifecycle))
            {
                return _dbContextDefaultLifecycle;
            }
            else
            {
                var configuredCycle = GetLifecycleForConfig(lifecycle);

                if (configuredCycle == null)
                {
                    return _dbContextDefaultLifecycle;
                }
                else
                {
                    return configuredCycle;
                }
            }
        }

        private ILifecycle GetLifecycleForConfig(string config)
        {
            switch (config)
            {
                case "Transiet":
                    return Lifecycles.Transient;
                    
                case "Singleton":
                    return Lifecycles.Singleton;

                case "ThreadLocal":
                    return Lifecycles.ThreadLocal;
                    
                case "Unique":
                    return Lifecycles.Unique;
            }
            
            return null;
        }
    }
}
