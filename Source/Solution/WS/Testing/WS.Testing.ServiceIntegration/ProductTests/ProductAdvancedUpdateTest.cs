using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Products;
using WS.Database.Domain.Tagging;
using WS.Logic.Products.Interface;
using WS.Logic.Products.Mappings;
using WS.Logic.Products.Objects;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.ProductTests
{
    [TestFixture]
    public class ProductAdvancedUpdateTest : ServiceIntegrationTestBase<IProductLogic>
    {
        public ProductAdvancedUpdateTest()
        {
            ProductLogicAutoMapperConfig.Map();
        }

        /// <summary>
        /// This test is build to verify that the system can cope with the following scenario:
        /// 
        /// 1. We have a product from categoy B (B has a parent Category A)
        /// 2. Category B inherits all tags from A and has an additional tag BTag
        /// 3. When switching product from Category B to Category A OK
        /// 4. When switching product from Category A to Category B - back the BTag is loaded but without value NOT OK
        /// 5. When refreshing the category is correctly set and the tag value is corectly reloaded.
        /// 
        /// </summary>
        [Test]
        public void ChangingProductCategory_WhichHasASpecificTagToAParentCategory_ShouldDisplayAllTagValuesFromChild()
        {
            // ===================== ARRANGE ===================== 
            //====================================================

            // we will have two categories
            // which will have a collection of tags
            var parentCategory = CreateCategory(GetUniqueString("Parent"), null);
            var childCategory = CreateCategory(GetUniqueString("Child"), parentCategory);

            // generate the manufacturer that will be used for the product testing
            var manufacturer = CreateManufacturer(GetUniqueString("ManufacturerName"));

            // generate tags for the categories
            var parentTagTypeList = GenerateTagTypesForCategory(parentCategory, 3);
            var childTagTypeList = GenerateTagTypesForCategory(childCategory, 1);


            var productOpObject = new ProductOperationObject()
            {
                Name = GetUniqueString("NewAdvancedUpdateProduct"),
                CategoryId = childCategory.Id,
                ManufacturerId = manufacturer.Id,
                PriceCurrent = 100.ToString(),
                PriceRegular = 150.ToString(),
                Category = new CategoryOperationObject()
                {
                    Id = childCategory.Id
                },
                Manufacturer = new ManufacturerOperationObject()
                {
                    Id = manufacturer.Id
                }
            };

            // ===================== ACT =========================
            //====================================================
            // PART 1 :
            // Create the product
            var createResult = GetServiceInstance().Create(productOpObject);

            // mid term assertion that the product is created and on read we have the specific number
            // of tag types based on the child category
            Assert.IsTrue(createResult.Success, "The product must successfully be created.");
            Assert.IsTrue(createResult.Data.Id != 0, "The product must be asigned a valid Id upon creation");

            var newCreatedEditProduct = GetServiceInstance().GetEdit(createResult.Data.Id).Data;

            // Run assertion set for tag types
            var fullSetOfTagTypes = new List<TagType>();
            fullSetOfTagTypes.AddRange(childTagTypeList);
            fullSetOfTagTypes.AddRange(parentTagTypeList);

            RunFullAssertionSet(newCreatedEditProduct, productOpObject, childCategory, manufacturer, fullSetOfTagTypes);

            // set values on all the properties for the product - both for the parent and child category 
            // at this point the product is on the child category
            SetValuesOnAllPropertiesOnProduct(newCreatedEditProduct);

            var updateTagValuesResult = GetServiceInstance().Update(newCreatedEditProduct);
            var updatedPropertyValuesForAllPropsProduct = updateTagValuesResult.Data;

            // run full asserts for the updated properties but also check values
            RunFullAssertionSet(updatedPropertyValuesForAllPropsProduct, productOpObject, childCategory, manufacturer, fullSetOfTagTypes, true);

            // Update/ACT by Changing the Category of the product to the parent category
            // and setting tag type values
            updatedPropertyValuesForAllPropsProduct.CategoryId = parentCategory.Id;
            updatedPropertyValuesForAllPropsProduct.Category.Id = parentCategory.Id;

            var updatedProductToParentCategory = GetServiceInstance().Update(updatedPropertyValuesForAllPropsProduct).Data;

            // assert the information again but expecting only the parents tag types
            RunFullAssertionSet(updatedProductToParentCategory, productOpObject, parentCategory, manufacturer, parentTagTypeList, true);
            
            // RUN THE MAIN ACTION - CHANGING BACK TO THE CHILD CATEGORY
            updatedProductToParentCategory.Category.Id = childCategory.Id;
            updatedProductToParentCategory.CategoryId = childCategory.Id;
            
            var finalUpdateResult =
                GetServiceInstance().Update(updatedProductToParentCategory);
            
            var productUpdatedBackToChildCategory = finalUpdateResult.Data;
            
            // ===================== ASSERT ===================== 
            // ==================================================
            RunFullAssertionSet(productUpdatedBackToChildCategory, productOpObject, childCategory, manufacturer, fullSetOfTagTypes, true);
        }

        #region Asserts

        private void RunFullAssertionSet(
            ProductEditObject readProduct,
            ProductOperationObject expectedProductInfo,
            Category expectedCategoryInfo,
            Manufacturer expectedManufacturerInfo,
            List<TagType> expectedTagList,
            bool runPropertyValueAsserts = false)
        {
            // product general asserts
            Assert.AreEqual(expectedProductInfo.Name, readProduct.Name, "The product name must match");
            Assert.AreEqual(expectedProductInfo.PriceCurrent, readProduct.PriceCurrent, "The product price current must match");
            Assert.AreEqual(expectedProductInfo.PriceRegular, readProduct.PriceRegular, "The product price regular must match");

            // assert manufacturer
            Assert.AreEqual(expectedManufacturerInfo.Id, readProduct.ManufacturerId, "The product must have the correct manufacturer set");
            Assert.IsNotNull(readProduct.Manufacturer);

            // assert product
            Assert.AreEqual(expectedCategoryInfo.Id, readProduct.CategoryId, "The Product must have the correct expected category");
            Assert.IsNotNull(readProduct.Category, "The category information for the product must be present");

            // assert tag types
            Assert.AreEqual(readProduct.Properties.Count, expectedTagList.Count, "The number of properties on the product must match the expected tag types present on the product");
            
            foreach (var tagType in expectedTagList)
            {
                var propertyOnProduct = readProduct.Properties.FirstOrDefault(p => p.PropertyTypeId == tagType.Id);

                Assert.IsNotNull(propertyOnProduct, $"The tag type with id {tagType.Id} must be contained in the list of properties for the product");
                Assert.IsTrue(propertyOnProduct.Name == tagType.Name, $"The name of the contained tag type {tagType.Name} must match the name of the property: {propertyOnProduct.Name}");

                if (runPropertyValueAsserts)
                {
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(propertyOnProduct.Value), $"The value on the property with name {propertyOnProduct.Name} must be set");
                }
            }
        }

        #endregion

        #region Test Data Generation Utilities

        /// <summary>
        /// Create, for the given category, a specific number of tag types.
        /// The tag types are returned in a list back to the calling client code
        /// </summary>
        /// <param name="category"></param>
        /// <param name="numberOfTagTypesToGenerate"></param>
        /// <returns></returns>
        private List<TagType> GenerateTagTypesForCategory(Category category, int numberOfTagTypesToGenerate)
        {
            var listOfTagTypes = new List<TagType>();
            var tagTypeService = GetUtilityService<ITagTypesLogic>();

            for (int i = 0; i < numberOfTagTypesToGenerate; i++)
            {
                var tagType = new TagType()
                {
                    Name = GetUniqueString(category.Name + "TagType" + i)
                };

                var created = tagTypeService.CreateTagForCategory(tagType, category.Id).Data;
                listOfTagTypes.Add(created);
            }

            return listOfTagTypes;
        }

        /// <summary>
        /// Create a manufacturer with the given name
        /// </summary>
        /// <param name="manufacturerName"></param>
        /// <returns></returns>
        private Manufacturer CreateManufacturer(string manufacturerName)
        {
            var manufacturer = new Manufacturer()
            {
                Name = manufacturerName
            };

            var created = GetUtilityService<IManufacturerLogic>().Create(manufacturer).Data;

            return created;
        }

        /// <summary>
        /// Create a category with the given name and optional parent
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Category CreateCategory(string categoryName, Category parent = null)
        {
            var category = new Category()
            {
                Name = categoryName
            };

            if (parent != null)
            {
                category.ParentId = parent.Id;
            }

            var createdCategory = GetUtilityService<ICategoryLogic>().CreateCategory(category).Data;

            return createdCategory;
        }

        #endregion

        #region Tag Type and Property Configuration

        private void SetValuesOnAllPropertiesOnProduct(ProductEditObject product)
        {
            foreach (var property in product.Properties)
            {
                property.Value = GetUniqueString("PropValue");
            }
        }

        private string SetPropertyValueForSpecificTagType(ProductEditObject product, int propertyTagTypeId)
        {
            var newValue = GetUniqueString("PropNewValue", 8);

            var property = product.Properties.FirstOrDefault(p => p.PropertyTypeId == propertyTagTypeId);
            if (property != null) property.Value = newValue;

            return newValue;
        }

        #endregion
    }
}
