using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Extensions;
using WS.Database.Access.Interface.Repositories;
using WS.Database.Domain.Base;
using WS.Database.Domain.Products;

namespace WS.Database.Access.Core.Repos
{
    public class ProductRepository : GenericRepository<Product, DbContext>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context)
        { }

        public IQueryable<Product> Query()
        {
            return Set
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Category.CategoryImage)
                .Include(p => p.Category.Parent)
                .Include(p => p.ProductTagValues)
                .Include(p => p.ProductImages).Include(p => p.ProductImages.Select(pi => pi.Image));
        }

        public Product GetById(int id, bool ignoreActive = false)
        {
            var productQueryable = Set
                .Include(p => p.Category)
                .Include(p => p.Category.CategoryImage)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductTagValues)
                .OnlyActive(ignoreActive)
                .Include(p => p.ProductImages).Include(p => p.ProductImages.Select(pi => pi.Image));
            
            var product = productQueryable.FirstOrDefault(p => p.Id == id);

            return product;
        }

        /// <summary>
        /// Insert the given product in the database
        /// </summary>
        /// <param name="productDomainObject"></param>
        /// <returns></returns>
        public Product Create(Product productDomainObject)
        {
            var newProduct = CreateEntity(productDomainObject);

            return newProduct;
        }

        /// <summary>
        /// Update the information for the given product in the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product Update(Product product)
        {
            // check product tag values and set the correct state
            foreach (var productTagValue in product.ProductTagValues)
            {
                Context.Entry(productTagValue).State = productTagValue.Id == 0 ? EntityState.Added : EntityState.Modified;
            }

            List<ProductImage> deletedImages = new List<ProductImage>();

            // check product tag values and set the correct state
            foreach (var productImage in product.ProductImages)
            {
                if (productImage.Id == 0)
                {
                    Context.Entry(productImage).State = EntityState.Added;
                    Context.Entry(productImage.Image).State = EntityState.Added;
                    StampNew(productImage);
                }
                else if (productImage.State == WsEntityState.Deleted)
                {
                    deletedImages.Add(productImage);
                    Context.Entry(productImage).State = EntityState.Deleted;
                }
                else
                {
                    Context.Entry(productImage).State = EntityState.Modified;
                    StampUpdate(productImage);
                    Context.Entry(productImage.Image).State = EntityState.Unchanged;
                }
            }

            foreach (var deletedImage in deletedImages)
            {
                product.ProductImages.Remove(deletedImage);
            }

            AttachAndModify(product);

            Context.Entry(product.Category).State = EntityState.Unchanged;
            Context.Entry(product.Manufacturer).State = EntityState.Unchanged;

            // reload the tag values based on the change in category
            Context.Entry(product).Collection(r => r.ProductTagValues).Load();

            return product;
        }
    }
}
