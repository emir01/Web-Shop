using NUnit.Framework;
using WS.Database.Bootstrap.Context;
using System.Collections.Generic;
using System.Diagnostics;
using WS.Database.Domain.Products;

namespace WS.Testing.DataAccess
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        public void MiscTest()
        {
            var context = new WsContext();

            var product = new Product()
            {
                Id = 1,
                ProductTagValues = new List<ProductTagValue> {
                    new ProductTagValue
                    {
                        Id = 1,
                        Value = "Test"
                    }
                }
            };

            context.Products.Attach(product);

            var state = context.Entry(product).State;

            Trace.WriteLine($"Product State {state}");

            context.Entry(product).Collection(p => p.ProductTagValues).Load();

            foreach (var tagValue in product.ProductTagValues)
            {
                Trace.Write($"State for tag value with id {tagValue.Id} is {context.Entry(tagValue).State}");
            }

            Assert.IsTrue(true);
        }
    }
}
