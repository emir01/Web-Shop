using NUnit.Framework;
using WS.Logic.Core.QueryContracts;
using WS.Logic.Products.Interface;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.ProductTests
{
    [TestFixture]
    public class ProductQueryTests:ServiceIntegrationTestBase<IProductLogic>
    {
        [Test]
        public void QueryProducts_Should_Return_All_Products()
        {
            // Arrange
            var productService = GetServiceInstance();
            
            // Act
            var productQueryResult = productService.Query(new ProductQueryContract());

            // Assert
            Assert.IsTrue(productQueryResult.Success);
        }
    }
}
