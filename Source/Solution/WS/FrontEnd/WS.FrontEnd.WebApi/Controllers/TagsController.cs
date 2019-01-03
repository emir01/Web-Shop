using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WS.Contracts.Contracts.Dtos.Tags;
using WS.Database.Domain.Tagging;
using WS.FrontEnd.WebApi.Infrastucture;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.Logic.Products.Interface;

namespace WS.FrontEnd.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class TagsController : ApiController
    {
        private readonly ITagTypesLogic _tagTypesLogic;

        public TagsController(ITagTypesLogic tagTypesLogic)
        {
            _tagTypesLogic = tagTypesLogic;
        }

        /// <summary>
        /// Return a collection of tags for the given category id
        /// </summary>
        /// <param name="categoryId">The id of the category for which we are retrieving tags</param>
        /// <param name="includeParentCategoryTags">If set to true will include the tag types defined for teh parent categories of the specified category </param>
        /// <returns></returns>
        [AllowAnonymous]
        public IEnumerable<TagTypeDto> Get(int? categoryId = null, bool includeParentCategoryTags = false)
        {
            var tagsQueryResult = _tagTypesLogic.GetTagTypes(categoryId, includeParentCategoryTags);

            if (tagsQueryResult.Success)
            {
                var tags = tagsQueryResult.Data.Select(AutoMapper.Mapper.Map<TagType, TagTypeDto>).ToList();

                return tags;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(tagsQueryResult));
        }

        /// <summary>
        /// Recieve a tag type dto from the body and create a new tag from the 
        /// dto. 
        /// 
        /// Return the newly created tag including its id to the client
        /// </summary>
        /// <param name="newTag"></param>
        /// <returns></returns>
        public TagTypeDto Post([FromBody] TagTypeDto newTag)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                // map from the new tag to a TagType domain obhject using auto mapper
                var tagTypeDomainObject = AutoMapper.Mapper.Map<TagTypeDto, TagType>(newTag);

                var tagCreateActionResult = _tagTypesLogic.CreateTagForCategory(tagTypeDomainObject, newTag.CategoryId);

                if (tagCreateActionResult.Success)
                {
                    // map the dto of the newly created tag returned form the service to an apropriate tag dto
                    // which we will return back to the client
                    var storedTagDto = AutoMapper.Mapper.Map<TagType, TagTypeDto>(tagCreateActionResult.Data);
                    return storedTagDto;
                }

                throw new HttpResponseException(
                    ResponseMessageBuilder.BuildMessageFromActionResult(tagCreateActionResult));
            });
        }

        /// <summary>
        /// Recieve a tag type dto from the body with a already existing id and update
        /// the tag information.
        /// 
        /// Returns the updated tag back to the client or null/exception if update failed
        /// </summary>
        /// <param name="updatedTag"></param>
        /// <returns></returns>
        public TagTypeDto Put([FromBody] TagTypeDto updatedTag)
        {
            return ActionVerbConfigService.WrapAction(() =>
                        {
                            var tagTypeDomainObject = AutoMapper.Mapper.Map<TagTypeDto, TagType>(updatedTag);

                            var tagUpdateActionResult = _tagTypesLogic.UpdateTag(tagTypeDomainObject);

                            if (tagUpdateActionResult.Success)
                            {
                                var updatedTagDto = AutoMapper.Mapper.Map<TagType, TagTypeDto>(tagUpdateActionResult.Data);
                                return updatedTagDto;
                            }

                            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(tagUpdateActionResult));
                        });
        }

        /// <summary>
        /// DeleteEntity the tag in the system with the given tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TagTypeDto Delete(int id)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var tagDeleteActionResult = _tagTypesLogic.DeleteTag(id);

                if (tagDeleteActionResult.Success)
                {
                    return AutoMapper.Mapper.Map<TagType, TagTypeDto>(tagDeleteActionResult.Data);
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(tagDeleteActionResult));
            });
        }
    }
}
