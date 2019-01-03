using System.Collections.Generic;
using System.Linq;
using WS.Database.Domain.Tagging;

namespace WS.Database.Access.Interface.Repositories
{
    public interface ITagTypeRepository
    {
        /// <summary>
        /// Return all the tags defined in the system
        /// </summary>
        /// <returns></returns>
        IQueryable<TagType> Query();

        /// <summary>
        /// Should return the tag types define only for the given category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IEnumerable<TagType> QueryByCategoryId(int categoryId);

        /// <summary>
        /// Return a given tag for the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TagType GetById(int id);

        /// <summary>
        /// Insert a new tag type into the set/database.
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        TagType Create(TagType tagType);

        /// <summary>
        /// Attach and update the tag type.
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        TagType Update(TagType tagType);
    }
}
