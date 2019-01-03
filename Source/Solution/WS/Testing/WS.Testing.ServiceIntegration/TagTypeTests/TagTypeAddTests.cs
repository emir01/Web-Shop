using NUnit.Framework;
using WS.Logic.Products;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.TagTypeTests
{
    [TestFixture]
    public class TagTypeAddTests : ServiceIntegrationTestBase<TagTypesLogic>
    {
        [Test]
        public void TagTypeLogic_Add_CreateTagType()
        {
            // Arrange
            var name = GetUniqueString("Tag Type");
            var uniqueAlias = GetUniqueString("Alias", 14);
            
            // this presumes there is always a category with id 1
            int categoryId = 1;
            
            var tagType = new Database.Domain.Tagging.TagType()
            {
                Name = name,
                Alias = uniqueAlias
            };

            // Act
            var service = GetServiceInstance();

            var createResult = service.CreateTagForCategory(tagType, categoryId);

            // Assert
            Assert.IsTrue(createResult.Success, "The result returned from calling CreateTagForCategory must be true");

            // get the tag types for the specified cateogry as atached to that category and not parent 
            // categories
            var createdTagType = GetServiceInstance().GetTagTypeForId(createResult.Data.Id).Data;

            Assert.IsNotNull(createdTagType, $"The new tag we tried to read from the collection of tags for the category with id {categoryId}");
            Assert.IsTrue(createdTagType.Name == name, "The expected tag type should have the provided name");
            
            AssertEntityCreated(createdTagType);
        }
    }
}
