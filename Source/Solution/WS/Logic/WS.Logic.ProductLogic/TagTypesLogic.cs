using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Interface;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Tagging;
using WS.Logic.Core.Results;
using WS.Logic.Products.Interface;

namespace WS.Logic.Products
{
    public class TagTypesLogic : ITagTypesLogic
    {
        private readonly IWsUnitOfWork _unitOfWork;

        public TagTypesLogic(IWsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Query all the tag types for the given category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="includeParentCategoryTags"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<TagType>> GetTagTypes(int? categoryId, bool includeParentCategoryTags)
        {
            try
            {
                if (!categoryId.HasValue)
                {
                    var allTagTypes = _unitOfWork
                        .TagTypeRepository.Query()
                        .Include(tt => tt.Categories)
                        .ToList();

                    return ActionResult<IEnumerable<TagType>>.GetSuccess(allTagTypes);
                }

                return GetTagTypesForCategory(categoryId.Value, includeParentCategoryTags);
            }
            catch (Exception ex)
            {
                return ActionResult<IEnumerable<TagType>>.GetFailed($"Exception: {ex.Message}");
            }
        }

        private ActionResult<IEnumerable<TagType>> GetTagTypesForCategory(int categoryId, bool includeParentCategoryTags)
        {
            var category = _unitOfWork.CategoryRepository.GetByCategoryId(categoryId);

            if (category == null)
            {
                return
                    ActionResult<IEnumerable<TagType>>.GetFailed(
                        $"[GetTagTypesForCategory] There is no category with the id {categoryId}");
            }

            // if there are no tag types for the category
            if (category.TagTypes == null)
            {
                return ActionResult<IEnumerable<TagType>>.GetSuccess(new List<TagType>());
            }

            var tags = _unitOfWork.TagTypeRepository.QueryByCategoryId(category.Id).ToList();

            if (includeParentCategoryTags)
            {
                var categoryParent = category.ParentId.HasValue ? _unitOfWork.CategoryRepository.GetByCategoryId(category.ParentId.Value) : null;

                while (categoryParent != null)
                {
                    var parentTags =
                        _unitOfWork.TagTypeRepository.QueryByCategoryId(categoryParent.Id).ToList();

                    tags.AddRange(parentTags);

                    categoryParent = categoryParent.Parent;
                }
            }

            return ActionResult<IEnumerable<TagType>>.GetSuccess(tags);
        }

        /// <summary>
        /// Returns a tag type object for the given id
        /// </summary>
        /// <param name="tagTypeId"></param>
        /// <returns></returns>
        public ActionResult<TagType> GetTagTypeForId(int tagTypeId)
        {
            try
            {
                var tagType = _unitOfWork.TagTypeRepository.GetById(tagTypeId);

                return ActionResult<TagType>.GetSuccess(tagType, "Retrieved Tag Type for a given specified id");
            }
            catch (Exception ex)
            {
                return ActionResult<TagType>.GetFailed(ex.Message);
            }
        }

        /// <summary>
        /// Create a new category with the given information for the given category id
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult<TagType> CreateTagForCategory(TagType tagType, int categoryId)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetByCategoryId(categoryId);

                tagType.Categories = new List<Category> { category };

                var createdTagType = _unitOfWork.TagTypeRepository.Create(tagType);

                _unitOfWork.Commit();

                return ActionResult<TagType>.GetSuccess(createdTagType);
            }

            catch (Exception ex)
            {
                return ActionResult<TagType>.GetFailed($"Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the tag given with the given tag type domain object.
        /// </summary>
        /// <param name="tagTypeDomainObject"></param>
        /// <returns></returns>
        public ActionResult<TagType> UpdateTag(TagType tagTypeDomainObject)
        {
            try
            {
                // make sure the tag type domain object has an id
                if (tagTypeDomainObject.Id == 0)
                {
                    throw new ArgumentException("The Tag Type Domain object to be udpated has an invalid id");
                }

                var tag = _unitOfWork.TagTypeRepository.Update(tagTypeDomainObject);

                // persist the changes
                _unitOfWork.Commit();

                // return the updated tag
                return ActionResult<TagType>.GetSuccess(tag);
            }
            catch (Exception ex)
            {
                return ActionResult<TagType>.GetFailed($"Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete the tag with the given id.
        /// 
        /// The returned result contains the tag that was just deleted from the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult<TagType> DeleteTag(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException($"The id provided for deleting a tag is not a valid id. Id: {id}");
                }

                var tagToDelete = _unitOfWork.TagTypeRepository.GetById(id);

                if (tagToDelete == null)
                {
                    throw new InvalidOperationException($"Trying to delete tag with id {id} which does not exist in the system");
                }

                var deletedTag = _unitOfWork.TagTypeRepository.Delete(tagToDelete);

                _unitOfWork.Commit();

                return ActionResult<TagType>.GetSuccess(deletedTag);
            }
            catch (Exception ex)
            {
                return ActionResult<TagType>.GetFailed($"Exception when deleting tag with id: {id}; Message: {ex.Message}");
            }
        }
    }
}