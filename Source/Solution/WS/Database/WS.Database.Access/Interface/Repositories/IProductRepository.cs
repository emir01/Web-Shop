using System.Linq;
using WS.Database.Domain.Products;

namespace WS.Database.Access.Interface.Repositories
{
    /// <summary>
    /// Define specific data access methods for product
    /// </summary>
    public interface IProductRepository
    {
        IQueryable<Product> Query();

        Product GetById(int id, bool onlyActive = false);

        Product Create(Product productDomainObject);

        Product Update(Product product);
    }
}
