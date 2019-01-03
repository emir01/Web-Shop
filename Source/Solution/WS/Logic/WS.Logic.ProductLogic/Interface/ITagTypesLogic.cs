using System.Collections.Generic;
using WS.Database.Domain.Tagging;
using WS.Logic.Core.Results;

namespace WS.Logic.Products.Interface
{
    /// <summary>
    /// Define the functionality for working with category tag types in the system
    /// </summary>
    public interface ITagTypesLogic
    {
        /// <summary>
        /// Query all the tag types for the given category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="includeParentCategoryTags"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<TagType>> GetTagTypes(int? categoryId, bool includeParentCategoryTags);

        /// <summary>
        /// Returns a tag type object for the given id
        /// </summary>
        /// <param name="tagTypeId"></param>
        /// <returns></returns>
        ActionResult<TagType> GetTagTypeForId(int tagTypeId);

        /// <summary>
        /// Create a new tag type with the given information for the given category id
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        ActionResult<TagType> CreateTagForCategory(TagType tagType, int categoryId);
        
        /// <summary>
        /// Update the tag given with the given tag type domain object.
        /// </summary>
        /// <param name="tagTypeDomainObject"></param>
        /// <returns></returns>
        ActionResult<TagType> UpdateTag(TagType tagTypeDomainObject);
        
        /// <summary>
        /// Delete the tag with the given id.
        /// 
        /// The returned result contains the tag that was just deleted from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult<TagType> DeleteTag(int id);
    }
}
