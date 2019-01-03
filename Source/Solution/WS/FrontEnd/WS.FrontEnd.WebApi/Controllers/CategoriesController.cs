using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WS.Contracts.Contracts.Dtos.Categories;
using WS.Database.Domain.Categorization;
using WS.FrontEnd.WebApi.Infrastucture;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.FrontEnd.WebApi.Infrastucture.FileManagement;
using WS.Logic.Products.Extensions;
using WS.Logic.Products.Interface;

namespace WS.FrontEnd.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoriesController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        // GET api/products
        [AllowAnonymous]
        public IEnumerable<CategoryHirearchyDto> Get()
        {
            var queryResult = _categoryLogic.GetRootLevelCategories();

            if (queryResult.Success)
            {
                var categoryDtos = queryResult.Data.Select(AutoMapper.Mapper.Map<Category, CategoryHirearchyDto>);

                return categoryDtos;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        public CategoryHirearchyDto Post(CategoryHirearchyDto root)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var domainCategoryRoot = AutoMapper.Mapper.Map<CategoryHirearchyDto, Category>(root);

                var actionResult = _categoryLogic.UpdateCategoryHirearchy(domainCategoryRoot);

                if (actionResult.Success)
                {
                    CleanupCategoryImages();
                    return AutoMapper.Mapper.Map<Category, CategoryHirearchyDto>(actionResult.Data);
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(actionResult));
            });
        }

        private void CleanupCategoryImages()
        {
            var activeCategoryImagesQuery = _categoryLogic.GetActiveCategoryImages();

            if (activeCategoryImagesQuery.Success)
            {
                new FileManager().ProcessAndDeleteFilesInDirectoriesUnderRoot("CategoryImages", activeCategoryImagesQuery.Data.Select(i => i.Uri).ToList());
            }
        }

        /// <summary>
        /// Return a flat category list containing all the categories in the system.
        /// 
        /// It is used to populate a dropdown list of categories in product management
        /// </summary>
        /// <returns></returns>
        [Route("api/categories/all")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<CategorySimpleDto> GetCategoryList()
        {
            var queryResult = _categoryLogic.GetCategories();

            if (queryResult.Success)
            {
                return queryResult.Data.Select(AutoMapper.Mapper.Map<Category, CategorySimpleDto>).ToList().SortByParentageAndAssignDepth();
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }
    }
}
