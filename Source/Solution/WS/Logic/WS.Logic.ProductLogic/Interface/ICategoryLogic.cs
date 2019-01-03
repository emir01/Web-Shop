using System.Collections.Generic;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Core;
using WS.Logic.Core.Results;

namespace WS.Logic.Products.Interface
{
    /// <summary>
    /// Handles data retrieval and core management of categories in the system
    /// </summary>
    public interface  ICategoryLogic
    {
        /// <summary>
        /// Return only root level categories. Given the specified system 
        /// only the Product category will be retrieved with all the other categories
        /// in the system contained in the children properties.
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Category>> GetRootLevelCategories();

        /// <summary>
        /// Returns a list of all categories in the system.
        /// 
        /// Each category contains the children and parent properties which can be excluded when returning the categories to the client
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Category>> GetCategories();

        /// <summary>
        /// Create/Save the provided category in storage.
        /// Returns the category that was created wrapped in a ActionResult with the id it now has in the database.
        /// </summary>
        /// <param name="categoryToCreate"></param>
        /// <returns></returns>
        ActionResult<Category> CreateCategory(Category categoryToCreate);
        
        /// <summary>
        /// Update the Hirearchy Root of Categories given with the domain category root that should
        /// point to the non-editable Product Category
        /// </summary>
        /// <param name="domainCategoryRoot"></param>
        /// <returns></returns>
        ActionResult<Category> UpdateCategoryHirearchy(Category domainCategoryRoot);
        
        /// <summary>
        /// Return the collection of active category images
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<AppImage>> GetActiveCategoryImages();
    }
}
