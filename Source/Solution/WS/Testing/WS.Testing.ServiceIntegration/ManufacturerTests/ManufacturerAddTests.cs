using System.Linq;
using NUnit.Framework;
using WS.Database.Domain.Categorization;
using WS.Logic.Products.Interface;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.ManufacturerTests
{
    [TestFixture]
    public class ManufacturerAddTests : ServiceIntegrationTestBase<IManufacturerLogic>
    {
        [Test]
        public void ManufacturerAdd_ShouldAddManufacturer()
        {
            // ARRANGE
            var manufacturer = new Manufacturer()
            {
                Name = GetUniqueString("New Manufacturer"),
                Alias = GetUniqueString("New Manufacture Alias", 10)
            };
            
            // ACT
            var result = GetServiceInstance().Create(manufacturer);

            var readManufacturer = GetServiceInstance().Get().Data.FirstOrDefault(m => m.Alias == manufacturer.Alias);

            // ASSERT
            Assert.IsTrue(result.Success);

            // assert that we have a read manufacturer
            Assert.IsNotNull(readManufacturer);
            Assert.AreEqual(manufacturer.Name, readManufacturer.Name);

            AssertEntityCreated(readManufacturer, assertExactTimeCreated: false);
        }
    }
}
