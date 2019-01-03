using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Interface;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Core;
using WS.Logic.Core.Results;
using WS.Logic.Products.Interface;

namespace WS.Logic.Products
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly IWsUnitOfWork _wsUnitOfWork;

        private readonly string CategoryAppimageTypeValue = "CategoryImage";

        public CategoryLogic(IWsUnitOfWork wsUnitOfWork)
        {
            _wsUnitOfWork = wsUnitOfWork;
        }

        /// <summary>
        /// Return only root level categories. Given the specified system 
        /// only the Product category will be retrieved with all the other categories
        /// in the system contained in the children properties.
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Category>> GetRootLevelCategories()
        {
            try
            {
                // only get root level categories
                // with all the child categories in the children property
                var categories = _wsUnitOfWork
                    .CategoryRepository
                    .Query()
                    .Include(c => c.Children)
                    .Include(c => c.Children.Select(child => child.CategoryImage))
                    .Include(c => c.CategoryImage)
                    .ToList();

                var rootOnly = categories.Where(t => t.ParentId == null).ToList();

                return new ActionResult<IEnumerable<Category>>
                {
                    Data = rootOnly,
                    Success = true,
                    Total = rootOnly.Count
                };
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Category>>
                {
                    Message = ex.Message,
                    Data = null,
                    Success = false
                };
            }
        }

        /// <summary>
        /// Returns a list of all categories in the system.
        /// 
        /// Each category contains the children and parent properties which can be excluded when returning the categories to the client
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            try
            {
                // get all the categories
                // this included tag types but probably not needed on the UI because of front end mapping
                // to simple dtos which do not include the tag types
                var categories = _wsUnitOfWork.CategoryRepository.Query();

                return ActionResult<IEnumerable<Category>>.GetSuccess(categories);
            }
            catch (Exception ex)
            {
                return ActionResult<IEnumerable<Category>>.GetFailed($"[GetCategories] - Exception when retrieving all categories. Ex Message: {ex.Message}");
            }
        }

        /// <summary>
        /// Create/Save the provided category in storage.
        /// Returns the category that was created wrapped in a ActionResult with the id it now has in the database.
        /// </summary>
        /// <param name="categoryToCreate"></param>
        /// <returns></returns>
        public ActionResult<Category> CreateCategory(Category categoryToCreate)
        {
            try
            {
                var createdCategory = _wsUnitOfWork.CategoryRepository.Create(categoryToCreate);
                _wsUnitOfWork.Commit();

                return ActionResult<Category>.GetSuccess(createdCategory, "Successfully create ");
            }
            catch (Exception ex)
            {
                return ActionResult<Category>.GetFailed($"[CreateCategory] Exception creating the category: " + ex.Message);
            }
        }

        /// <summary>
        /// Update the Hirearchy Root of Categories given with the domain category root that should
        /// point to the non-editable Product Category
        /// </summary>
        /// <param name="domainCategoryRoot"></param>
        /// <returns></returns>
        public ActionResult<Category> UpdateCategoryHirearchy(Category domainCategoryRoot)
        {
            try
            {
                var updatedCategoryRoot = _wsUnitOfWork.CategoryRepository.UpdateCategoryHirearchy(domainCategoryRoot);
                _wsUnitOfWork.Commit();

                return ActionResult<Category>.GetSuccess(updatedCategoryRoot);
            }
            catch (Exception ex)
            {
                return ActionResult<Category>.GetFailed($"Exception updating category hirearchy via root: {ex.Message}");
            }
        }

        /// <summary>
        /// Return the collection of active category images
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<AppImage>> GetActiveCategoryImages()
        {
            try
            {
                var activeCategroyImages =
                    _wsUnitOfWork.AppImageRepository.QueryActiveImagesForType(CategoryAppimageTypeValue);
                
                return ActionResult<IEnumerable<AppImage>>.GetSuccess(activeCategroyImages);
            }
            catch (Exception ex)
            {
                return ActionResult<IEnumerable<AppImage>>.GetFailed($"Exception while retrieving active category images: {ex.Message}");
            }
        }
    }
}