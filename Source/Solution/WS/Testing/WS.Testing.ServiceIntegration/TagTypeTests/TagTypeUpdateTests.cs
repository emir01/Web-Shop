using System.Linq;
using NUnit.Framework;
using WS.Database.Domain.Tagging;
using WS.Logic.Products;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.TagTypeTests
{
    [TestFixture]
    public class TagTypeUpdateTests : ServiceIntegrationTestBase<TagTypesLogic>
    {
        [Test]
        public void UpdateTagType_ShouldUpdateTagType()
        {
            //======================================================
            // ARRANGE
            //======================================================
            // get the tag we are going to be updating
            // describe 
            var categoryId = 2;
            
            var tagWeWillUpdate = GetServiceInstance().GetTagTypes(categoryId, false).Data.FirstOrDefault();
            
            Assert.IsNotNull(tagWeWillUpdate, $"Integration test must retrieve a tag that will be updated");
            var idOfTagToUpdate = tagWeWillUpdate.Id;
            
            var newTagName = GetUniqueString("NewTagName", 10);

            //======================================================
            // ACT
            //======================================================
            // update the name
            tagWeWillUpdate.Name = newTagName;
            
            //get a new service instance and run update
            var updateResult = GetServiceInstance().UpdateTag(tagWeWillUpdate);

            //======================================================
            // ASSERT
            //======================================================
            Assert.IsTrue(updateResult.Success, " The result from calling update must be a success");

            // get the updated tag using the id 
            var updatedTag = GetServiceInstance().GetTagTypeForId(idOfTagToUpdate).Data;

            Assert.IsNotNull(updatedTag, "The tag that was updated must be correctly read again to make asserts");

            // make the primary assert that the tag has an updated name proprety
            Assert.AreEqual(newTagName, updatedTag.Name, "The tag name must match between the updated value and what was read from the database");

            // make sure the tag type still is linked to the category
            Assert.IsTrue(updatedTag.Categories != null && updatedTag.Categories.Count > 0 && updatedTag.Categories.Any(c => c.Id == categoryId), "The updated tag type must retain the categories");
            
            AssertEntityUpdated(updatedTag);
        }

        /// <summary>
        /// This is a full fledget integration and behaviour test for the system that covers Entity Framework detached updates
        /// for Tag Types in relation to categories
        /// </summary>
        [Test]
        public void UpdateTagType_Should_ProperlyUpdateFromDetachedState()
        {
            //======================================================
            // ARRANGE
            //======================================================
            // to have this test run without any problems we are first going to be creating a new tag type 
            // for a given randomly selected category;

            var category = GetUtilityService<CategoryLogic>().GetCategories().Data.FirstOrDefault();

            Assert.IsNotNull(category);
            
            // create the tag type we will be updating
            var tagType = new TagType()
            {
                Name = "Original Name",
                CreatedBy = "System"
            };
            
            var createResult = GetServiceInstance().CreateTagForCategory(tagType, category.Id);

            Assert.IsTrue(createResult.Success, $"Before updating a tag type the tag type must be created in the database for the category with id {category.Id}");

            var createdTag = GetServiceInstance().GetTagTypeForId(createResult.Data.Id).Data;

            AssertEntityCreated(createdTag);

            Assert.IsNotNull(createdTag, $"We must be able to read the newly created tag type from the database based on its id {createResult.Data.Id}");
            Assert.IsTrue(createdTag.Categories.Any(c => c.Id == category.Id), "The newly created tag we read from the database must be asigned to the category we created it for");

            // create a tag type object that will be used to simulate detached updates
            // the mock tag type will contain some of the key properties for the tag type we want to run updates on
            // this will run only with the key pieces of information.
            // does not contain the info regarding atachment to categories and we expect that to remain the same when we read the 
            // tag type again
            var detachedTagTypeToUpdate = new TagType()
            {
                Id = createdTag.Id,
                Name = GetUniqueString("UpdatedName")
            };

            //======================================================
            // ACT
            //======================================================
            var updatedTagResult = GetServiceInstance().UpdateTag(detachedTagTypeToUpdate);
            
            //======================================================
            // ASSERT
            //======================================================
            Assert.IsTrue(updatedTagResult.Success, "The result from runnin the update operation from detached tag should be a success");
            
            // re-read the tag 
            var updateTag = GetServiceInstance().GetTagTypeForId(createdTag.Id).Data;
            
            Assert.AreEqual(detachedTagTypeToUpdate.Name, updateTag.Name, "The name of the updated tag must match");
            
            Assert.IsNotNull(updateTag, "The updated tag should not be null");
            Assert.IsTrue(updateTag.Categories.Any(c => c.Id == category.Id), "The updated tag should keep its categories");
            
            // run the meta data asserts
            AssertEntityCreated(updateTag, true);
            AssertEntityUpdated(updateTag);
        }
    }
}
