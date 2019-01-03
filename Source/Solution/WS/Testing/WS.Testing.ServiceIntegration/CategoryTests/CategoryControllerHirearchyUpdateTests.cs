using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using WS.Database.Domain.Base;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Tagging;
using WS.Logic.Products.Interface;
using WS.Testing.ServiceIntegration.Base;

namespace WS.Testing.ServiceIntegration.CategoryTests
{
    [TestFixture]
    public class CategoryHirearchyUpdateTests : ServiceIntegrationTestBase<ICategoryLogic>
    {
        [Test]
        public void UpdateHirearchy_AddingChildCategoryShouldAddNewChildCategory()
        {
            // ARRANGE 
            // we are going to be working with the already existing category hirearch in the db. 

            // get the current category hirearchy

            var category = GetServiceInstance().GetRootLevelCategories().Data.ToList().First();

            // add a child category 
            var newCategory = new Category()
            {
                Name = GetUniqueString("ADD_CH: NewCategory", 30),
                ParentId = category.Id
            };

            category.Children.Add(newCategory);

            // ACT 
            // try and save the new category hirearch
            var result = GetServiceInstance().UpdateCategoryHirearchy(category);

            // ASSERT
            Assert.IsTrue(result.Success, "The call to UpdateCategoryHirearchy must be a success");

            // re read the category hirearch
            var updateRootCategory = GetServiceInstance().GetRootLevelCategories().Data.First();
            var newRootChild = updateRootCategory.Children.FirstOrDefault(c => c.Name == newCategory.Name);
            Assert.IsNotNull(newRootChild, "The updated category hirearch root must contain the new category");

            // CLEANUP
            newRootChild.State = WsEntityState.Deleted;
            GetServiceInstance().UpdateCategoryHirearchy(updateRootCategory);
        }

        [Test]
        public void UpdateHirearchy_Adding_Multiple_ChildCategories_MultipleChildCategories()
        {
            // ARRANGE 
            // we are going to be working with the already existing category hirearch in the db. 

            // get the current category hirearchy
            var category = GetServiceInstance().GetRootLevelCategories().Data.ToList().First();

            // add a child category 
            var newCategory = new Category()
            {
                Name = GetUniqueString("ADD_MULT: NewCategory", 30),
                ParentId = category.Id,
                Children = new List<Category>()
            };

            var newChildOfNewCategory = new Category
            {
                Name = GetUniqueString("ADD_MULT: NewChildOfNewCategory", 30)
            };
            newCategory.Children.Add(newChildOfNewCategory);

            category.Children.Add(newCategory);

            // ACT 
            // try and save the new category hirearch
            var result = GetServiceInstance().UpdateCategoryHirearchy(category);

            // ASSERT
            Assert.IsTrue(result.Success, "The call to UpdateCategoryHirearchy must be a success");

            // re read the category hirearch
            var updatedCategoryRoot = GetServiceInstance().GetRootLevelCategories().Data.First();

            var firstChild = updatedCategoryRoot.Children.FirstOrDefault(c => c.Name == newCategory.Name);
            Assert.IsNotNull(firstChild, "The updated category hirearch root must contain the new category");

            // assert that the initial root child has its own child
            var childOfChild = firstChild.Children.FirstOrDefault(c => c.Name == newChildOfNewCategory.Name);
            Assert.IsNotNull(childOfChild, "The newly created child category must have its own child created");

            // CLEANUP
            firstChild.State = WsEntityState.Deleted;
            GetServiceInstance().UpdateCategoryHirearchy(updatedCategoryRoot);
        }

        [Test]
        public void UpdateHirearchy_AddingChildCategoryAndUpdatingExistingCategory()
        {
            // ARRANGE
            var category = GetServiceInstance().GetRootLevelCategories().Data.First();

            var modifiedCategory = category.Children[0];

            // the added category
            var newCategory = new Category()
            {
                Name = GetUniqueString("ADD_UPDT-ADD: New Category Name", 30),
                ParentId = modifiedCategory.Id
            };

            modifiedCategory.Name = GetUniqueString("ADD_UPDT-UPDT: Updated Name", 30);
            modifiedCategory.Children.Add(newCategory);

            // ACT
            var result = GetServiceInstance().UpdateCategoryHirearchy(category);

            // ASSERT
            Assert.IsTrue(result.Success, "The result from the category hirearchy update must be true");

            // find the child category
            var updatedRoot = GetServiceInstance().GetRootLevelCategories().Data.First();
            var updatedCategory = updatedRoot.Children[0];

            Assert.AreEqual(modifiedCategory.Name, updatedCategory.Name, "The Name of the new read updated category must match the expected modified name");
            var updateCategoryChild = updatedCategory.Children.FirstOrDefault(c => c.Name == newCategory.Name);
            Assert.IsNotNull(updateCategoryChild, "The new added category must be present in the list of children for the modified category");

            // CLEANUP
            updateCategoryChild.State = WsEntityState.Deleted;
            GetServiceInstance().UpdateCategoryHirearchy(updatedRoot);
        }

        [Test]
        public void UpdateHirearchy_RemovingCategoryFromHirearchy_ShouldRemoveCategory()
        {
            // ARRANGE
            // first create a new category to remove including with its own little sub category
            var originalRoot = GetServiceInstance().GetRootLevelCategories().Data.First();

            var firstChildOfRoot = new Category()
            {
                ParentId = originalRoot.Id,
                Name = GetUniqueString("DEL:Root Child New", 30)
            };

            // just to generate the id to add a new leaf node category to the Root Child New
            originalRoot.Children.Add(firstChildOfRoot);
            GetServiceInstance().UpdateCategoryHirearchy(originalRoot);

            var rootWithExtraNode = GetServiceInstance().GetRootLevelCategories().Data.First();
            var rootChild = rootWithExtraNode.Children.First(c => c.Name == firstChildOfRoot.Name);

            var leafNode = new Category
            {
                Name = GetUniqueString("DEL:Leaf Node Of Root Child", 30),
                ParentId = rootChild.Id
            };

            rootChild.Children = new List<Category> { leafNode };

            // save the new hirearch 
            GetServiceInstance().UpdateCategoryHirearchy(rootWithExtraNode);

            // ACT
            // get the final update root state
            var finalUpdateRootState = GetServiceInstance().GetRootLevelCategories().Data.First();

            // make sure everyting is in order
            var rootChildPreDelete = finalUpdateRootState.Children.FirstOrDefault(c => c.Name == firstChildOfRoot.Name);
            Assert.IsNotNull(rootChildPreDelete, "The Direct child to the root must be in the final root state Children collection - Before Delete");

            var leafPreDelete = rootChildPreDelete.Children.FirstOrDefault(c => c.Name == leafNode.Name);
            Assert.IsNotNull(leafPreDelete, "The Leaf Node of the Direct Root Child must be present in the Root Pre Delete State");

            // remove the root child node
            rootChildPreDelete.State = WsEntityState.Deleted;

            // DO THE ACTUAL IMPORTANT CALL
            var mainActionServiceInstance = GetServiceInstance();
            var result = mainActionServiceInstance.UpdateCategoryHirearchy(finalUpdateRootState);

            // ASSERT
            Assert.IsTrue(result.Success, "After making the final update category hirearchy removing a root  child we expect the result to be success");

            var rootWithRemovedNode = GetServiceInstance().GetRootLevelCategories().Data.First();
            Assert.IsTrue(rootWithRemovedNode.Children.All(c => c.Name != firstChildOfRoot.Name), "We should not be able to see the category node we deleted in a category hirearchy after removing and updating the hirearchu");
        }

        [Test]
        public void UpdateHirearchy_RemovingNewlyAddedCategoryFromHirearchy_ShouldDoNothing()
        {
            // ARRANGE
            // first create a new category to remove including with its own little sub category
            var originalRoot = GetServiceInstance().GetRootLevelCategories().Data.First();

            var firstChildOfRoot = new Category()
            {
                ParentId = originalRoot.Id,
                Name = GetUniqueString("DEL: Root Child New", 30)

            };

            // just to generate the id to add a new leaf node category to the Root Child New
            originalRoot.Children.Add(firstChildOfRoot);

            GetServiceInstance().UpdateCategoryHirearchy(originalRoot);

            var rootWithExtraNode = GetServiceInstance().GetRootLevelCategories().Data.First();

            var rootChild = rootWithExtraNode.Children.First(c => c.Name == firstChildOfRoot.Name);

            var leafNode = new Category
            {
                Name = GetUniqueString("DEL:Leaf Node Of Root Child", 30),
                ParentId = rootChild.Id
            };

            rootChild.Children = new List<Category> { leafNode };

            // save the new hirearch 
            GetServiceInstance().UpdateCategoryHirearchy(rootWithExtraNode);

            // ACT
            // get the final update root state

            var finalUpdateRootState = GetServiceInstance().GetRootLevelCategories().Data.First();

            // make sure everyting is in order
            var rootChildPreDelete = finalUpdateRootState.Children.FirstOrDefault(c => c.Name == firstChildOfRoot.Name);
            Assert.IsNotNull(rootChildPreDelete, "The Direct child to the root must be in the final root state Children collection - Before Delete");

            var leafPreDelete = rootChildPreDelete.Children.FirstOrDefault(c => c.Name == leafNode.Name);
            Assert.IsNotNull(leafPreDelete, "The Leaf Node of the Direct Root Child must be present in the Root Pre Delete State");

            // remove the root child node
            rootChildPreDelete.State = WsEntityState.Deleted;

            // ######################################################################
            // CRITICAL: Adding a new categroy to the root which we mark as deletedf at the same time should have no effect
            // ######################################################################
            var finalRootStateDeleteAddChild = new Category()
            {
                ParentId = finalUpdateRootState.Id,
                Name = GetUniqueString("DEL: Child with Deleted State", 30),
                State = WsEntityState.Deleted
            };

            finalUpdateRootState.Children.Add(finalRootStateDeleteAddChild);

            // DO THE ACTUAL IMPORTANT CALL

            var mainActionServiceInstance = GetServiceInstance();
            var result = mainActionServiceInstance.UpdateCategoryHirearchy(finalUpdateRootState);

            // ASSERT
            Assert.IsTrue(result.Success, "After making the final update category hirearchy removing a root  child we expect the result to be success");

            var rootWithRemovedNode = GetServiceInstance().GetRootLevelCategories().Data.First();

            Assert.IsTrue(rootWithRemovedNode.Children.All(c => c.Name != firstChildOfRoot.Name), "We should not be able to see the category node we deleted in a category hirearchy after removing and updating the hirearchu");
            Assert.IsTrue(rootWithRemovedNode.Children.All(c => c.Name != finalRootStateDeleteAddChild.Name), "The root must not contain the finally added category which was added (0 id) and then removed - State set");
        }
    }
}
