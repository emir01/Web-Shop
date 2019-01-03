using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WS.Contracts.Contracts.Dtos.Products;
using WS.Database.Domain.Products;
using WS.FrontEnd.WebApi.Infrastucture;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.FrontEnd.WebApi.Infrastucture.FileManagement;
using WS.FrontEnd.WebApi.Models;
using WS.Logic.Core.QueryContracts;
using WS.Logic.Products.Interface;
using WS.Logic.Products.Objects;

namespace WS.FrontEnd.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ProductsController : ApiController
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        // GET api/products
        public IEnumerable<ProductDto> Get([FromUri]ProductQuery query)
        {
            CheckQueryForLoggedAdmin(query);

            var mappedQuery = AutoMapper.Mapper.Map<ProductQueryContract>(query);

            var queryResult = _productLogic.Query(mappedQuery);

            if (queryResult.Success)
            {
                var productDtos = queryResult.Data.Select(AutoMapper.Mapper.Map<ProductDto>).ToList();
                return productDtos;
            }

            return null;
        }

        private void CheckQueryForLoggedAdmin(ProductQuery query)
        {
            if (!User.Identity.IsAuthenticated)
            {
                query.IgnoreStatus = false;
            }
        }

        // GET api/products/related/5
        [Route("api/products/related/{id}")]
        public IEnumerable<ProductDto> GetRelatedProducts(int id)
        {
            var queryResult = _productLogic.GetRelatedProducts(id);

            if (queryResult.Success)
            {
                var productDtos = queryResult.Data.Select(AutoMapper.Mapper.Map<ProductDto>).ToList();
                return productDtos;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        // GET api/products/compare
        [Route("api/products/compare")]
        [HttpPost]
        public EntitiesCompareObject<ProductEditObject> GetCompareProducts([FromBody] List<int> idValues)
        {
            var queryResult = _productLogic.QueryEntitiesCompareObject(idValues);

            if (queryResult.Success)
            {
                return queryResult.Data;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        // GET api/products/5
        public ProductEditObject Get(int id)
        {
            var queryResult = _productLogic.GetEdit(id);

            if (queryResult.Success)
            {
                return queryResult.Data;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        [Route("api/products/edit/{id}")]
        [Authorize]
        [HttpGet]
        public ProductEditObject GetProductEdit(int id)
        {
            var queryResult = _productLogic.GetEdit(id);

            if (queryResult.Success)
            {
                return queryResult.Data;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        /// <summary>
        ///  Create the product in the system and return a simple dto for
        /// the created product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        public ProductAdminDto Post(ProductOperationObject product)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var createResult = _productLogic.Create(product);

                if (createResult.Success)
                {
                    var createdProduct = createResult.Data;
                    var adminDto = AutoMapper.Mapper.Map<ProductAdminDto>(createdProduct);
                    return adminDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(createResult));
            });
        }

        // PUT api/products/5
        public ProductEditObject Put(ProductEditObject product)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var updateResult = _productLogic.Update(product);

                if (updateResult.Success)
                {
                    CheckAndDeleteImages(product);

                    return updateResult.Data;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(updateResult));
            });
        }

        // DELETE api/products/5
        [Authorize]
        public ProductDto Delete(int id)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var deleteResult = _productLogic.Delete(id);

                if (deleteResult.Success)
                {
                    var productDto = AutoMapper.Mapper.Map<Product, ProductDto>(deleteResult.Data);
                    return productDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(deleteResult));
            });
        }

        // DELETE api/products/5
        [Authorize]
        [HttpPut]
        [Route("api/products/activate")]
        public ProductDto Activate([FromBody]int id)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var activateResult = _productLogic.Activate(id);

                if (activateResult.Success)
                {
                    var productDto = AutoMapper.Mapper.Map<Product, ProductDto>(activateResult.Data);
                    return productDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(activateResult));
            });
        }

        private void CheckAndDeleteImages(ProductEditObject product)
        {
            new FileManager().ProcessAndDeleteFilesInDirectory($"ProductImages\\{product.Id}", product.ProductImages.Select(s => s.Image.Uri).ToList());
        }
    }
}
