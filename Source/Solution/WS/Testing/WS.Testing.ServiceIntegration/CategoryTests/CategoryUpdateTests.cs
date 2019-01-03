using System.Linq;
using NUnit.Framework;
using WS.Database.Access.Interface.Repositories;
using WS.Logic.Products.Interface;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.CategoryTests
{
    [TestFixture]
    public class CategoryUpdateTests : ServiceIntegrationTestBase<ICategoryLogic>
    {
        [Test]
        public void Update_Category_ShouldNotAlter_TagTypeStateForCategory()
        {
            var categoryWeAreEditingAlias = "ctgr_bikes";
            /*  
                Description: This tests starts with the initial Web Shop Test database
                             and attempts to update the Bikes Category. When doing so
                             we expect there to be no changes to the tag types collection
                             of the updated category. This proved to be a problem when 
                             working with UpdateHirearchy which is also used here in a more 
                             specific way.

            */

            // ARRANGE
            var categoryRoot = GetServiceInstance().GetRootLevelCategories().Data.First();
            var categoryWeAreEditing = categoryRoot.Children.First(c => c.Alias == categoryWeAreEditingAlias);

            // change the name and record the number of tag types related to the category
            var updatedName = categoryWeAreEditing.Name.Split(' ')[0] + " " + GetUniqueString("Update", 5);
            categoryWeAreEditing.Name = updatedName;

            // get the category via its id to count the number of tag types
            var category = GetUtilityService<ICategoryRepository>().GetByCategoryId(categoryWeAreEditing.Id);
            var tagTypeCountOriginal = category.TagTypes.Count;

            // ACT
            // make the main update call
            GetServiceInstance().UpdateCategoryHirearchy(categoryRoot);

            // ASSERT
            var root = GetServiceInstance().GetRootLevelCategories().Data.First();
            var categoryThatWasUpdated = root.Children.First(c => c.Alias == categoryWeAreEditingAlias);

            Assert.AreEqual(updatedName, categoryThatWasUpdated.Name, "The name of the category must be updated");

            // get the category once again via the utility service - the repo
            var fullUpdatedCategory = GetUtilityService<ICategoryRepository>().GetByCategoryId(categoryWeAreEditing.Id);
            Assert.AreEqual(tagTypeCountOriginal, fullUpdatedCategory.TagTypes.Count, "The Number of Tag Types for the category must stay the same");
}
    }
}
