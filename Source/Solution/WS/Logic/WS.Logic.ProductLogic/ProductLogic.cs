using System;
using System.Collections.Generic;
using System.Linq;
using WS.Database.Access.Extensions;
using WS.Database.Access.Interface;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Products;
using WS.Logic.Core.QueryContracts;
using WS.Logic.Core.Results;
using WS.Logic.Products.Interface;
using WS.Logic.Products.Objects;

namespace WS.Logic.Products
{
    public class ProductLogic : IProductLogic
    {
        private const int SimilarProductCount = 3;

        private readonly IWsUnitOfWork _unitOfWork;

        private readonly ITagTypesLogic _tagTypeLogic;

        public ProductLogic(IWsUnitOfWork unitOfWork, ITagTypesLogic tagTypesLogic)
        {
            _unitOfWork = unitOfWork;

            _tagTypeLogic = tagTypesLogic;
        }

        /// <summary>
        /// Query products.
        /// </summary>
        /// <param name="queryContract"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Product>> Query(ProductQueryContract queryContract)
        {
            try
            {
                var products = _unitOfWork
                                         .ProductRepository
                                         .Query()
                                         .OnlyActive(queryContract.IgnoreStatus)
                                         .ToList();

                products = FilterName(queryContract, products);

                products = FilterCategory(queryContract, products);

                products = FilterManufacturer(queryContract, products);

                products = FilterMinPrice(queryContract, products);

                products = FitlerMaxPrice(queryContract, products);

                products = FilterTagValues(queryContract, products);

                products = FilterSpecialQueries(queryContract, products);

                products = Sort(queryContract, products);

                var enumeratedProducts = products as IList<Product> ?? products.ToList();
                var count = enumeratedProducts.Count;

                return new ActionResult<IEnumerable<Product>>
                {
                    Data = enumeratedProducts,
                    Total = count,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Product>>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Return related products to the product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<Product>> GetRelatedProducts(int productId)
        {
            try
            {
                // get the initial product
                var product = _unitOfWork.ProductRepository.GetById(productId);

                if (product == null)
                {
                    return ActionResult<IEnumerable<Product>>.GetSuccess(new List<Product>());
                }

                List<Product> similarProductList = new List<Product>();
                int missingSimilarProductsCount = SimilarProductCount;

                var relatedProductsByCategory = _unitOfWork
                    .ProductRepository
                    .Query()
                    .Where(p => p.CategoryId == product.CategoryId && p.Id != productId)
                    .ToList();

                // similar by category

                similarProductList.AddRange(relatedProductsByCategory.Count >= missingSimilarProductsCount
                    ? relatedProductsByCategory.Take(missingSimilarProductsCount)
                    : relatedProductsByCategory);

                missingSimilarProductsCount -= similarProductList.Count;

                // similar by manufacturer
                if (missingSimilarProductsCount > 0)
                {
                    var relatedProductsByManufacturer = _unitOfWork
                   .ProductRepository
                   .Query()
                   .Where(p => p.ManufacturerId == product.ManufacturerId)
                   .ToList();

                    similarProductList.AddRange(relatedProductsByManufacturer.Count >= missingSimilarProductsCount
                        ? relatedProductsByManufacturer.Take(missingSimilarProductsCount)
                        : relatedProductsByManufacturer);

                    missingSimilarProductsCount -= similarProductList.Count;
                }

                // add any products
                if (missingSimilarProductsCount > 0)
                {
                    var randomProducts = _unitOfWork
                   .ProductRepository
                   .Query().ToList();

                    similarProductList.AddRange(randomProducts.Take(missingSimilarProductsCount));
                }

                return ActionResult<IEnumerable<Product>>.GetSuccess(similarProductList);
            }
            catch (Exception ex)
            {
                return new ActionResult<IEnumerable<Product>>
                {
                    Data = null,
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        private List<Product> FilterSpecialQueries(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.OnSale)
            {
                products =
                    products.Where(p => p.PriceCurrent.HasValue && p.PriceCurrent < p.PriceRegular && p.PriceRegular > 0);
            }

            return products.ToList();
        }

        private List<Product> Sort(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            switch (queryContract.Sort)
            {
                case SortOptions.NameAscending:
                    return products.OrderBy(p => p.Name).ToList();
                case SortOptions.NameDescending:
                    return products.OrderByDescending(p => p.Name).ToList();
                case SortOptions.PriceAscending:
                    return products.OrderBy(p => p.PriceCurrent != null && p.PriceCurrent != 0 ? p.PriceCurrent : p.PriceRegular).ToList();
                case SortOptions.PriceDescending:
                    return products.OrderByDescending(p => p.PriceCurrent != null && p.PriceCurrent != 0 ? p.PriceCurrent : p.PriceRegular).ToList();
                default:
                    return products.ToList();
            }
        }

        private List<Product> FilterTagValues(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.TagFilters.Any())
            {
                foreach (var tagFilter in queryContract.TagFilters)
                {
                    products = products.Where(p => p.ProductTagValues.Any(tv => tv.TagTypeId == tagFilter.TagTypeId && tv.Value.Contains(tagFilter.FilterValue)));
                }
            }

            return products.ToList();
        }

        private static List<Product> FitlerMaxPrice(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.MaxPrice.HasValue)
            {
                products = products.Where(p => (p.PriceCurrent.HasValue && p.PriceCurrent <= queryContract.MaxPrice) || (!p.PriceCurrent.HasValue && p.PriceRegular <= queryContract.MaxPrice));
            }

            return products.ToList();
        }

        private static List<Product> FilterMinPrice(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.MinPrice.HasValue)
            {
                products = products.Where(p => (p.PriceCurrent.HasValue && p.PriceCurrent >= queryContract.MinPrice) || (!p.PriceCurrent.HasValue && p.PriceRegular >= queryContract.MinPrice));
            }

            return products.ToList();
        }

        private static List<Product> FilterManufacturer(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.ManufacturerId.HasValue)
            {
                products = products.Where(p => p.ManufacturerId == queryContract.ManufacturerId.Value);
            }
            return products.ToList();
        }

        private List<Product> FilterCategory(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (queryContract.CategoryId.HasValue)
            {
                products = from p in products where p.Category.Id == queryContract.CategoryId.Value || GetParentCollectionOfIdValues(p).Contains(queryContract.CategoryId.Value) select p;
            }
            return products.ToList();
        }

        private static List<Product> FilterName(ProductQueryContract queryContract, IEnumerable<Product> products)
        {
            if (!string.IsNullOrWhiteSpace(queryContract.Name))
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(queryContract.Name.ToLower()));
            }

            return products.ToList();
        }

        private List<int> GetParentCollectionOfIdValues(Product product)
        {
            var list = new List<int>();

            var category = product.Category;

            while (category?.ParentId != null)
            {
                list.Add(category.ParentId.Value);
                category = category.Parent;
            }

            return list;
        }

        /// <summary>
        /// Return a collection of products for comparison for the given product id values
        /// </summary>
        /// <param name="productIdValues"></param>
        /// <returns></returns>
        public ActionResult<IEnumerable<ProductEditObject>> QueryProductsForCompare(List<int> productIdValues)
        {
            try
            {
                var productEditObjects = new List<ProductEditObject>();

                foreach (var productIdValue in productIdValues)
                {
                    Product product = _unitOfWork.ProductRepository.GetById(productIdValue);
                    int productCategoryId = product.Category.Id;
                    List<ProductProperty> properties = GetAllProductProperties(productCategoryId, product);

                    var productEditObject = AutoMapper.Mapper.Map<ProductEditObject>(product);
                    productEditObject.Properties = properties;

                    productEditObjects.Add(productEditObject);
                }

                return ActionResult<IEnumerable<ProductEditObject>>.GetSuccess(productEditObjects);
            }
            catch (Exception ex)
            {
                return ActionResult<IEnumerable<ProductEditObject>>.GetFailed($"[QueryProductsForCompare] Exception/Failed to retreive products for comparison. Exception: {ex.Message}");
            }
        }

        public ActionResult<Product> Delete(int id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetById(id);

                if (product == null)
                {
                    return ActionResult<Product>.GetFailed("There is no such product");
                }


                _unitOfWork.ProductRepository.DisableEntity(product);
                _unitOfWork.Commit();

                return ActionResult<Product>.GetSuccess(product, "Product deactivated");
            }
            catch (Exception ex)
            {
                return ActionResult<Product>.GetFailed($"[DeleteProduct] Exception/Failed when deleting/deactivating product. Exception: {ex.Message}");
            }
        }

        public ActionResult<Product> Activate(int id)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetById(id, true);

                if (product == null)
                {
                    return ActionResult<Product>.GetFailed("There is no such product");
                }

                product.Status = true;

                _unitOfWork.ProductRepository.EnableEntity(product);
                _unitOfWork.Commit();

                return ActionResult<Product>.GetSuccess(product, "Product deactivated");
            }
            catch (Exception ex)
            {
                return ActionResult<Product>.GetFailed($"[DeleteProduct] Exception/Failed when deleting/deactivating product. Exception: {ex.Message}");
            }
        }

        public ActionResult<EntitiesCompareObject<ProductEditObject>> QueryEntitiesCompareObject(List<int> productIdValues)
        {
            try
            {
                ActionResult<IEnumerable<ProductEditObject>> productEditObjectsCompare = QueryProductsForCompare(productIdValues);

                if (productEditObjectsCompare.Success)
                {
                    var entitiesCompareObject = GetProductEntitiyCompareObject();

                    foreach (var productEditObject in productEditObjectsCompare.Data)
                    {
                        entitiesCompareObject.Entities.Add(productEditObject);
                        AddProductToEntitiesCompare(productEditObject, entitiesCompareObject);
                    }

                    return ActionResult<EntitiesCompareObject<ProductEditObject>>.GetSuccess(entitiesCompareObject);
                }
                else
                {
                    return ActionResult<EntitiesCompareObject<ProductEditObject>>.GetFailed(productEditObjectsCompare.Message);
                }
            }
            catch (Exception ex)
            {
                return ActionResult<EntitiesCompareObject<ProductEditObject>>.GetFailed($"[QueryEntitiesCompareObject] Exception/Failed to retreive Entities Compare Object. Exception: {ex.Message}");
            }
        }

        private void AddProductToEntitiesCompare(ProductEditObject productEditObject, EntitiesCompareObject<ProductEditObject> entitiesCompareObject)
        {
            var entityId = productEditObject.Id.ToString();

            entitiesCompareObject.AddValue(entityId, "Name", productEditObject.Name).AddValue(entityId, "Description", productEditObject.Description).AddValue(entityId, "Category", productEditObject.Category.Name).AddValue(entityId, "Manufacturer", productEditObject.Manufacturer.Name).AddValue(entityId, "PriceRegular", productEditObject.PriceRegular).AddValue(entityId, "PriceCurrent", productEditObject.PriceCurrent);
        }

        private EntitiesCompareObject<ProductEditObject> GetProductEntitiyCompareObject()
        {
            var productEntitiesCompareObject = new EntitiesCompareObject<ProductEditObject>();

            return productEntitiesCompareObject.AddCompareProperty("Name").AddCompareProperty("Description").AddCompareProperty("Category").AddCompareProperty("Manufacturer").AddCompareProperty("PriceRegular", "currency").AddCompareProperty("PriceCurrent", "currency");
        }

        /// <summary>
        /// Returns a product for editing. This means that it includes a list of all tags
        /// for the category of the product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult<ProductEditObject> GetEdit(int productId)
        {
            try
            {
                // get the product for the given id
                var product = _unitOfWork.ProductRepository.GetById(productId, true);

                // map the product to the 
                var coreObject = AutoMapper.Mapper.Map<ProductEditObject>(product);

                MapCategoryPath(coreObject);

                coreObject.Properties = new List<ProductProperty>();

                // get the id of the category for the product
                var proudctCategoryId = product.Category.Id;

                var properties = GetAllProductProperties(proudctCategoryId, product);

                coreObject.Properties = properties;

                return ActionResult<ProductEditObject>.GetSuccess(coreObject);
            }
            catch (Exception ex)
            {
                return ActionResult<ProductEditObject>.GetFailed($"[GetEditProduct] Exception/Failed to retreive product with specified id {productId}. Exception: {ex.Message}");
            }
        }

        private void MapCategoryPath(ProductEditObject coreObject)
        {
            var categories = _unitOfWork.CategoryRepository.Query().ToList();

            var parent = categories.FirstOrDefault(c => c.Id == coreObject.Category.Id);

            while (parent != null)
            {
                coreObject.CategoryHirearchy.Add(AutoMapper.Mapper.Map<Category, CategoryOperationObject>(parent));
                parent = categories.FirstOrDefault(c => c.Id == parent.ParentId);
            }

            coreObject.CategoryHirearchy.Reverse();
        }

        /// <summary>
        /// Return a single raw product object for the given id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult<Product> GetRaw(int productId)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetById(productId);

                return ActionResult<Product>.GetSuccess(product, "Successfully retrieved product object");
            }
            catch (Exception ex)
            {
                return ActionResult<Product>.GetFailed(ex.Message);
            }
        }

        /// <summary>
        /// Create a domain product object from the given base product operations object.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ActionResult<Product> Create(ProductOperationObject product)
        {
            try
            {
                // map the product operation object to a domain object
                var productDomainObject = AutoMapper.Mapper.Map<ProductOperationObject, Product>(product);

                var newProduct = _unitOfWork.ProductRepository.Create(productDomainObject);

                _unitOfWork.Commit();

                newProduct.Category = new Category
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                };

                newProduct.Manufacturer = new Manufacturer
                {
                    Id = product.Manufacturer.Id,
                    Name = product.Manufacturer.Name
                };

                return ActionResult<Product>.GetSuccess(newProduct);
            }
            catch (Exception ex)
            {
                return ActionResult<Product>.GetFailed($"[GetEditProduct] Exception/Failed to create product. Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the informatin in the database for the given product
        /// </summary>
        /// <param name="productEditObject"></param>
        /// <returns></returns>
        public ActionResult<ProductEditObject> Update(ProductEditObject productEditObject)
        {
            try
            {
                // map the product core object to the product domain object
                var product = AutoMapper.Mapper.Map<ProductEditObject, Product>(productEditObject);

                // we want to update/set the tag values that actually have a value set
                product.ProductTagValues = product.ProductTagValues.Where(ptv => !string.IsNullOrWhiteSpace(ptv.Value)).ToList();

                var updatedProduct = _unitOfWork.ProductRepository.Update(product);

                _unitOfWork.Commit();

                // map back to the core object
                var coreObject = AutoMapper.Mapper.Map<Product, ProductEditObject>(updatedProduct);

                // get and set the properties for the new category on the updated product
                var properties = GetAllProductProperties(updatedProduct.CategoryId, updatedProduct);
                coreObject.Properties = properties;

                return ActionResult<ProductEditObject>.GetSuccess(coreObject);
            }
            catch (Exception ex)
            {
                return ActionResult<ProductEditObject>.GetFailed($"[Update Product] Exception/Failed when updating product. Exception message: {ex.Message}");
            }
        }

        private List<ProductProperty> GetAllProductProperties(int proudctCategoryId, Product product)
        {
            var properties = new List<ProductProperty>();
            // getall the tag types
            var allProductTagTypesResult = _tagTypeLogic.GetTagTypes(proudctCategoryId, true);

            if (!allProductTagTypesResult.Success)
            {
                throw new Exception($"Failed to get all tag types for product. Message:{allProductTagTypesResult.Message}");
            }

            // we need to create the information based on all the tag types for the product
            foreach (var productTagType in allProductTagTypesResult.Data)
            {
                var property = new ProductProperty { Name = productTagType.Name, PropertyTypeId = productTagType.Id };

                // check if the property already has value in the product TagValues - where for
                // each tag type for the product we store the value
                var productTagValueForTagType = product.ProductTagValues.FirstOrDefault(tv => tv.TagTypeId == productTagType.Id);

                if (productTagValueForTagType != null)
                {
                    property.Value = productTagValueForTagType.Value;
                    property.Id = productTagValueForTagType.Id;
                }

                properties.Add(property);
            }

            return properties;
        }
    }
}
