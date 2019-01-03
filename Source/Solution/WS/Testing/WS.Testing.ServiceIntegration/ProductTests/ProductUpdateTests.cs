using System.Linq;
using NUnit.Framework;
using WS.Logic.Core.Results;
using WS.Logic.Products.Interface;
using WS.Logic.Products.Mappings;
using WS.Logic.Products.Objects;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.ProductTests
{
    /// <summary>
    /// Test out the integrated functionality of the services working around product editing.
    /// </summary>
    [TestFixture]
    public class ProductUpdateTests : ServiceIntegrationTestBase<IProductLogic>
    {
        public ProductUpdateTests()
        {
            ProductLogicAutoMapperConfig.Map();
        }
        
        /// <summary>
        /// This is a simple update test.
        /// </summary>
        [Test]
        public void UpdateProduct_Should_UpdateSpecificProduct()
        {
            //======================================================
            // ARRANGE
            //======================================================

            // should create a unique stack of product and category to work on update
            // for now we are going to presume that we are using a seeded database which already has the objects we want
            // pre seeded

            int updatedProductId = 2;
            
            var service = GetServiceInstance();
            var productToUdpateData = service.GetEdit(updatedProductId).Data;

            //======================================================
            // ACT
            //======================================================
            
            // perform the update
            var newName = GetUniqueString("Update Name ");
            var newPropertyName = GetUniqueString("UpdateSomeProperty");
            
            productToUdpateData.Properties[1].Value = newPropertyName;
            productToUdpateData.Name = newName;
            
            var updateResult = ExecuteMethodUnderTest(productToUdpateData);

            //======================================================
            // ASSERT
            //======================================================

            Assert.IsTrue(updateResult.Result.Success);

            // check if the product has been updated
            service = GetServiceInstance();
            var queryUpdateProductResult = service.GetRaw(updatedProductId);

            // the product should have the same name and it should have a property
            // with the name of updated property
            Assert.IsTrue(queryUpdateProductResult.Success);

            var updatedProduct = queryUpdateProductResult.Data;

            Assert.AreEqual(newName, updatedProduct.Name, $"We expect the product from the database with name {updatedProduct.Name} to have the new name {newName}");

            AssertEntityCreated(updatedProduct, assertExactTimeCreated: false);
            AssertEntityUpdated(updatedProduct);
        }
        
        /*
            Creating a new more complicated update test would mean creating a new tag type for the product category.
                1. After creating a new tag for the category for which the product does not have a value:
                2. We insert a new Property with the new TagType Id and Some Value
                3. Verify that the product has been correctly updated.
        */

        [Test]
        public void UpdateProduct_BySettingAValueForTag_ForWhichTheProduct_DidNotHaveValueBefore()
        {
            //======================================================
            // ARRANGE
            //======================================================

            // the id of the product with which we will be working with 
            var productId = 2;
            
            // get the product and check its category
            // so we can create a new tag for the category
            var productWeWillEdit = GetServiceInstance().GetEdit(productId).Data;
            
            var categoryId = productWeWillEdit.Category.Id;

            // create a new tag type that will be atached t
            var newTagTypeName = GetUniqueString("TType");
            var tagType = new Database.Domain.Tagging.TagType { Name = newTagTypeName };
            
            var newCreatedTag = GetUtilityService<ITagTypesLogic>().CreateTagForCategory(tagType, categoryId).Data;
            
            // update the product we will edit
            var newTagValueForProduct = GetUniqueString("New Tag VAlue");
            var updatedTagValueForProduct = GetUniqueString("Existing Updated Tag Value");
            var updatedProductName = GetUniqueString("Updated Name");
            
            // first edit a current tag/property for the given product
            var existingProperty = productWeWillEdit.Properties.FirstOrDefault();
            if (existingProperty != null)
            {
                existingProperty.Value = updatedTagValueForProduct;
            }

            // add a property for which the product has not had an value so far
            productWeWillEdit.Properties.Add(new ProductProperty
            {
                PropertyTypeId = newCreatedTag.Id,
                Value = newTagValueForProduct
            });

            // change the name of the product
            productWeWillEdit.Name = updatedProductName;

            //======================================================
            // ACT
            //======================================================

            // update the product now
            var methodUnderTestResult = ExecuteMethodUnderTest(productWeWillEdit);

            //======================================================
            // ASSERT
            //======================================================
            // get the number of tag types for the category for which the product belongs to:
            var tagsProductShouldHave = GetUtilityService<ITagTypesLogic>().GetTagTypes(categoryId, true).Data.ToList();

            // re-read the product and check that the values are all set
            var updatedProduct = methodUnderTestResult.Result.Data;
            
            var readUpdatedProduct = GetServiceInstance().GetEdit(productId).Data;

            // Assert that the number of tag types the product has as it was returned from the edit action
            // matches the number of tag types for its categroy
            Assert.AreEqual(tagsProductShouldHave.Count, updatedProduct.Properties.Count);
            Assert.AreEqual(tagsProductShouldHave.Count, readUpdatedProduct.Properties.Count);

            // assert on the product returned from the update call
            RunUpdateProductAsserts("[FromUpdate]", updatedProduct, updatedProductName, existingProperty, updatedTagValueForProduct, newTagTypeName, newTagValueForProduct);

            RunUpdateProductAsserts("[CleanRead]", readUpdatedProduct, updatedProductName, existingProperty, updatedTagValueForProduct, newTagTypeName, newTagValueForProduct);
        }
        
        private void RunUpdateProductAsserts(string assertPrefix, ProductEditObject updatedProduct, string updatedProductName, ProductProperty existingProperty, string updatedTagValueForProduct, string newTagTypeName, string newTagValueForProduct)
        {
            // check to see if the product has been corectly update
            // first check for the name
            Assert.AreEqual(updatedProductName, updatedProduct.Name, $"{assertPrefix} The name of the product which is now {updatedProduct.Name} must match the expected name {updatedProductName}");

            // if there was an existing property
            if (existingProperty != null)
            {
                // check to see if the existing property has been updated
                var property = updatedProduct.Properties.FirstOrDefault(pr => pr.Id == existingProperty.Id);

                Assert.IsNotNull(property, $"{assertPrefix} The existing property which we updated must exist again");
                Assert.AreEqual(updatedTagValueForProduct, property.Value, $"{assertPrefix} The property {property.Name} has the value {property.Value} but we expect it to have the value {updatedTagValueForProduct}");
            }

            // check if the product has the new property
            var productNewlyCreatedProperty = updatedProduct.Properties.FirstOrDefault(pr => pr.Name == newTagTypeName);
            Assert.IsNotNull(productNewlyCreatedProperty);
            Assert.AreEqual(newTagValueForProduct, productNewlyCreatedProperty.Value, $"{assertPrefix} We are expecting the new created value/tag for the product: {productNewlyCreatedProperty.Name} to have the value {newTagValueForProduct}, but it has value: {productNewlyCreatedProperty.Value}");
        }

        #region Running Test

        /// <summary>
        /// Execute the main method under the test for the current integration set of tests
        /// </summary>
        /// <param name="productToEdit"></param>
        /// <returns></returns>
        private ProductServiceUpdateExecuteResult ExecuteMethodUnderTest(ProductEditObject productToEdit)
        {
            var service = GetServiceInstance();
            var result = service.Update(productToEdit);

            return new ProductServiceUpdateExecuteResult
            {
                Result = result,
                ServiceUnderTest = service
            };
        }

        private class ProductServiceUpdateExecuteResult
        {
            public ActionResult<ProductEditObject> Result { get; set; }

            public IProductLogic ServiceUnderTest { get; set; }
        }

        #endregion
    }
}
