using System.Linq;
using WS.Database.Domain.Categorization;

namespace WS.Database.Access.Interface.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Return a queryable of all the categories in the system
        /// </summary>
        /// <returns></returns>
        IQueryable<Category> Query();

        /// <summary>
        /// Return a single category for the given category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Category GetByCategoryId(int categoryId);

        /// <summary>
        /// Update the hirearchichal object graph given with the hirearchy root.
        /// </summary>
        /// <param name="hirearchyRoot"></param>
        /// <returns></returns>
        Category UpdateCategoryHirearchy(Category hirearchyRoot);
    }
}