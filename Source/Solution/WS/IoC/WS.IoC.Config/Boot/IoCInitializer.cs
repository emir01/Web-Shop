using StructureMap;
using StructureMap.Graph;
using System.Data.Entity;
using WS.Database.Access.Core;
using WS.Database.Access.Interface;
using WS.Database.Bootstrap.Context;
using WS.Logic.Core.Validators;
using WS.Logic.Products.Interface;

namespace WS.IoC.Config.Boot
{
    public static class IoCInitializer
    {
        public static IContainer Initialize()
        {
            // configure lifescopes for the WsUnitOfWork and WsContextObjects
            var bootconfigParameters = new BootConfigParameters();
            
            var container = new Container(x =>
            {
                x.Scan(scan =>
                    {
                        scan.TheCallingAssembly();

                        scan.AssemblyContainingType(typeof(IWsUnitOfWork));

                        scan.AssemblyContainingType(typeof(IQueryObjectValidator));

                        scan.AssemblyContainingType(typeof(IProductLogic));

                        scan.LookForRegistries();

                        scan.WithDefaultConventions();
                    }
                );
                
                // define the unit of work and how it is created, and its always supposed to be a singleton
                // there should not be more than one unit of work objects
                x.For<IWsUnitOfWork>().Use<WsUnitOfWork>().SetLifecycleTo(bootconfigParameters.GetUnitOfWorkLifecycle());
                
                // for any db context use the default WsContext with the default construct
                x.For<DbContext>().Use<WsContext>().SelectConstructor(() => new WsContext()).SetLifecycleTo(bootconfigParameters.GetDbContextLifecycle());
            });

            return container;
        }
    }
}