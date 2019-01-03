using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Extensions;
using WS.Database.Access.Interface.Repositories;
using WS.Database.Domain.Base;
using WS.Database.Domain.Categorization;

namespace WS.Database.Access.Core.Repos
{
    public class CategoryRepository : GenericRepository<Category, DbContext>, ICategoryRepository
    {
        public CategoryRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Return a list of all the categories in the system
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> Query()
        {
            return Set;
        }

        /// <summary>
        /// Return a single category for the given category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetByCategoryId(int categoryId)
        {
            var category = Set
                .Include(c => c.TagTypes)
                .Include(c => c.Parent)
                .Include(c => c.CategoryImage)
                .FirstOrDefault(c => c.Id == categoryId);

            if (category != null)
            {
                Context.Entry(category).Collection(c => c.TagTypes).Load();
                Context.Entry(category).Reload();
            }

            return category;
        }

        public Category Create(Category categoryToCreate)
        {
            var created = CreateEntity(categoryToCreate);
            return created;
        }

        /// <summary>
        /// Update the hirearchichal object graph given with the hirearchy root.
        /// </summary>
        /// <param name="hirearchyRoot"></param>
        /// <returns></returns>
        public Category UpdateCategoryHirearchy(Category hirearchyRoot)
        {
            Set.Add(hirearchyRoot);

            Context.Entry(hirearchyRoot).State = EntityState.Unchanged;

            // Category Updates
            List<Category> deleted = new List<Category>();

            SetStateOnChildCategoriesAndPopulateDeleted(hirearchyRoot, deleted);

            SetCategoryStatesAsDeleted(deleted);

            SetStateOnCategoryImagesAndPopulateDeleted(hirearchyRoot);

            return hirearchyRoot;
        }
        
        /// <summary>
        /// TODO: Move to utility library
        /// </summary>
        /// <param name="currentCategory"></param>
        private void SetStateOnCategoryImagesAndPopulateDeleted(Category currentCategory)
        {
            if (currentCategory.CategoryImage != null)
            {
                var currentCategoryImage = currentCategory.CategoryImage;
                
                // its new
                if (currentCategoryImage.IsNew())
                {
                    Context.Entry(currentCategoryImage).State = EntityState.Added;
                }
                // otherwise its modified
                else if (currentCategoryImage.IsModified())
                {
                    Context.Entry(currentCategoryImage).State = EntityState.Modified;
                }
                // if it is makred as deleted its deleted
                else if (currentCategoryImage.IsMarkedDeleted())
                {
                    Context.Entry(currentCategoryImage).State = EntityState.Deleted;
                }
                else
                {
                    Context.Entry(currentCategoryImage).State = EntityState.Unchanged;
                }
            }

            // call for all child categories and their images
            var childrenCount = currentCategory.Children.Count;

            for (int i = 0; i < childrenCount; i++)
            {
                var childCategory = currentCategory.Children[i];
                SetStateOnCategoryImagesAndPopulateDeleted(childCategory);
            }
        }

        /// <summary>
        /// Paint the state of the Category hirearchy. 
        /// TODO: Move to utility library that deals with state painting of hirearchichal entities with generic callbacks for state modification
        /// </summary>
        /// <param name="currentCategory"></param>
        private void SetStateOnChildCategoriesAndPopulateDeleted(Category currentCategory, List<Category> deleted, bool deletedParent = false)
        {
            if (currentCategory.Children != null && currentCategory.Children.Any())
            {
                var childrenCount = currentCategory.Children.Count;

                for (int i = 0; i < childrenCount; i++)
                {
                    var category = currentCategory.Children[i];

                    if (category == null)
                    {
                        continue;
                    }

                    if (deletedParent)
                    {
                        SetStateOnChildCategoriesAndPopulateDeleted(category, deleted, true);
                        deleted.Add(category);
                        continue;
                    }

                    if (category.State == WsEntityState.Deleted)
                    {
                        SetStateOnChildCategoriesAndPopulateDeleted(category, deleted, true);
                        deleted.Add(category);
                    }
                    else if (category.Id == 0)
                    {
                        // new
                        Context.Entry(category).State = EntityState.Added;
                        StampNew(category);
                        SetStateOnChildCategoriesAndPopulateDeleted(category, deleted);
                    }
                    else
                    {
                        //update
                        Context.Entry(category).State = EntityState.Modified;
                        StampUpdate(category);
                        SetStateOnChildCategoriesAndPopulateDeleted(category, deleted);
                    }
                }
            }
        }
    }
}