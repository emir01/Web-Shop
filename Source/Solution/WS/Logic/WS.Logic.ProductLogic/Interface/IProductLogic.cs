using System.Collections.Generic;
using WS.Database.Domain.Products;
using WS.Logic.Core.QueryContracts;
using WS.Logic.Core.Results;
using WS.Logic.Products.Objects;

namespace WS.Logic.Products.Interface
{
    /// <summary>
    /// Describe product related business logic functions.
    /// </summary>
    public interface IProductLogic
    {
        /// <summary>
        /// Query products.
        /// </summary>
        /// <param name="queryContract"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Product>> Query(ProductQueryContract queryContract);

        /// <summary>
        /// Return related products to the product 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<Product>> GetRelatedProducts(int productId);

        /// <summary>
        /// Return a collection of products for comparison for the given product id values
        /// </summary>
        /// <param name="productIdValues"></param>
        /// <returns></returns>
        ActionResult<IEnumerable<ProductEditObject>> QueryProductsForCompare(List<int> productIdValues);

        /// <summary>
        /// Return a single raw product object for the given id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ActionResult<Product> GetRaw(int productId);

        /// <summary>
        /// Returns a product for editing. This means that it includes a list of all tags
        /// for the category of the product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ActionResult<ProductEditObject> GetEdit(int productId);

        /// <summary>
        /// Create a domain product object from the given base product operations object.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        ActionResult<Product> Create(ProductOperationObject product);

        /// <summary>
        /// Update the informatin in the database for the given product
        /// </summary>
        /// <param name="productEditObject"></param>
        /// <returns></returns>
        ActionResult<ProductEditObject> Update(ProductEditObject productEditObject);

        ActionResult<Product> Delete(int id);

        ActionResult<Product> Activate(int id);

        ActionResult<EntitiesCompareObject<ProductEditObject>> QueryEntitiesCompareObject(
            List<int> productIdValues);
    }
}
