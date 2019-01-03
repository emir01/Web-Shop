using System;
using NUnit.Framework;
using WS.Logic.Products.Interface;
using WS.Logic.Products.Mappings;
using WS.Logic.Products.Objects;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.ProductTests
{
    [TestFixture]
    public class ProductAddTests : ServiceIntegrationTestBase<IProductLogic>
    {
        public ProductAddTests()
        {
            ProductLogicAutoMapperConfig.Map();
        }
        
        [Test] 
        public void CreateProduct_ShouldCreate_NewProduct()
        {
            // ===========================================
            // ARRANGE
            // ===========================================
            var productService = GetServiceInstance();
            
            var name = "Test Product Name" + Guid.NewGuid();
            
            var prodcutOperationObject = new ProductOperationObject()
            {
                Name = name,
                
                Category = new CategoryOperationObject()
                {
                    Id = 1
                },
                
                Manufacturer = new ManufacturerOperationObject()
                {
                    Id = 1
                }
            };

            // ===========================================
            // ACT
            // ===========================================

            var createProductResult = productService.Create(prodcutOperationObject);

            // ===========================================
            // ASSERT
            // ===========================================
            Assert.IsTrue(createProductResult.Success);
            
            var createdProduct =
                GetServiceInstance().GetEdit(createProductResult.Data.Id).Data;

            Assert.IsNotNull(createdProduct, "The product we queried from the database must not be null");
            Assert.AreEqual(name, createdProduct.Name);
            
            Assert.IsTrue(!string.IsNullOrWhiteSpace(createdProduct.Category.Name));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(createdProduct.Manufacturer.Name));
        }
    }
}
