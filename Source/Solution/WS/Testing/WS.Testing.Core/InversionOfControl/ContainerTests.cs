using NUnit.Framework;
using WS.Database.Access.Core;
using WS.Database.Access.Interface;
using WS.IoC.Container;

namespace WS.Testing.Core.InversionOfControl
{
    public class ContainerTests
    {
        [Test]
        public void Initialization_Of_Container()
        {
            // arrange
            var container = CWrapper.Container;
            
            // act
            var instanceOfWsUnitOfWork = container.GetInstance<IWsUnitOfWork>();

            //assert
            Assert.NotNull(instanceOfWsUnitOfWork);
            Assert.IsTrue(instanceOfWsUnitOfWork.GetType().IsAssignableFrom(typeof(WsUnitOfWork)));
        }
    }
}
