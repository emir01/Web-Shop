using System.Linq;
using NUnit.Framework;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Tagging;
using WS.Logic.Products.Interface;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.CategoryTests
{
    [TestFixture]
    public class CategoryAddTests : ServiceIntegrationTestBase<ICategoryLogic>
    {
        [Test]
        public void CategoryAdd_ShouldAddCategory()
        {
            // Arrange
            var category = new Category()
            {
                Name = GetUniqueString("Cat_Name"),
                Alias = GetUniqueString("Cat_Alias", 10)
            };

            var service = GetServiceInstance();

            // Act
            var categorySaveResult = service.CreateCategory(category);

            // Assert
            Assert.IsTrue(categorySaveResult.Success);

            // create a new isntance of the service and read the category with the given id
            // the newly created category which has no parent should be at the root level
            var readNewCategory = GetServiceInstance().GetRootLevelCategories().Data.FirstOrDefault(c => c.Alias == category.Alias);

            Assert.IsNotNull(readNewCategory);
            Assert.AreEqual(category.Name, readNewCategory.Name);

            AssertEntityCreated(readNewCategory, assertExactTimeCreated: false);
        }

        [Test]
        public void CategoryAdd_CreateCategoryUnderParent()
        {
            // Arrange
            var parentCategory = new Category
            {
                Name = GetUniqueString("CategoryName"),
                Alias = GetUniqueString("CategoryAlias", 10)
            };

            var createdParentCategory = GetServiceInstance().CreateCategory(parentCategory).Data;

            var childCategory = new Category()
            {
                Name = GetUniqueString("ChildCategory"),
                Alias = GetUniqueString("ChildCategoryAlias", 10),
                ParentId = createdParentCategory.Id
            };

            // Act
            var result = GetServiceInstance().CreateCategory(childCategory);

            // Assert
            Assert.IsTrue(result.Success, "The Create Categoy action must be a success when creating Category Under Parent");
            Assert.IsNotNull(result.Data, "The Create Category action must contain a not null data - The created category under the parent");

            var rootCategory = GetServiceInstance().GetRootLevelCategories().Data.FirstOrDefault(c => c.Alias == parentCategory.Alias);

            Assert.IsNotNull(rootCategory, "After creating a parent and child category we must find the parent in the root categories");
            Assert.IsTrue(rootCategory.Children.Count() == 1, "The root parent category must have exactly one child.");

            var childFromRoot = rootCategory.Children.FirstOrDefault(c => c.Alias == childCategory.Alias && c.Name == childCategory.Name);

            Assert.IsNotNull(childFromRoot, "The child category retrieved from the root category must have expected set values");

            AssertEntityCreated(rootCategory, assertExactTimeCreated: false);
            AssertEntityCreated(childFromRoot, assertExactTimeCreated: false);
        }

        [Test]
        public void CategoryAdd_CategoryShouldBeSavedTogetherWithTags()
        {
            // Arrange
            var tagOne = new TagType { Name = GetUniqueString("TagTypeOne") };
            var tagTwo = new TagType { Name = GetUniqueString("TagTypeTwo") };

            var category = new Category
            {
                Name = GetUniqueString("TagCategory", 10),
                Alias = GetUniqueString("TagCategoryAlias", 10)
            };

            // Act Part One
            var createdCategory = GetServiceInstance().CreateCategory(category).Data;

            // Act Part Two
            // add two tags for the category
            GetUtilityService<ITagTypesLogic>().CreateTagForCategory(tagOne, createdCategory.Id);
            GetUtilityService<ITagTypesLogic>().CreateTagForCategory(tagTwo, createdCategory.Id);
            
            // Assert
            // get the category and make sure there are 
            var readCategory = GetServiceInstance().GetCategories().Data.FirstOrDefault(c => c.Alias == category.Alias);
            Assert.IsNotNull(readCategory, "The category with the created containing the tags must be in the system");

            Assert.IsNotNull(readCategory, "The newly created category must be in the root categories");
            Assert.AreEqual(2, readCategory.TagTypes.Count, "The newly created category must have two tag types");

            var readTagOne = readCategory.TagTypes.FirstOrDefault(tt => tt.Name == tagOne.Name);
            var readTagTwo = readCategory.TagTypes.FirstOrDefault(tt => tt.Name == tagTwo.Name);

            Assert.IsNotNull(readTagOne, "The created category must contain TagOne");
            Assert.IsNotNull(readTagTwo, "The created category must contain TagTwo");

            AssertEntityCreated(readCategory, assertExactTimeCreated: false);
            AssertEntityCreated(tagOne, assertExactTimeCreated: false);
            AssertEntityCreated(tagOne, assertExactTimeCreated: false);
        }
    }
}
