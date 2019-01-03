using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using WS.Database.Bootstrap.Context;
using WS.Database.Domain.Products;
using static System.Console;

namespace WS.FrontEnd.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new WsContext();

            var product = new Product()
            {
                Id = 1,
                PriceCurrent = 1000,
                ProductTagValues = new List<ProductTagValue> {
                    new ProductTagValue
                    {
                        Id = 1,
                        TagTypeId = 1,
                        Value = "Test",
                    }
                }
            };

            context.Products.Attach(product);
            
            WriteLine("Calling reload on the product after doing an attach");
            
            //context.Entry(product).Reload();
            
            WriteLine($"Product Price Current from Entity {product.PriceCurrent}");

            WriteLine($"Product Price Current from Original Values {context.Entry(product).OriginalValues.GetValue<double>("PriceCurrent")}");

            WriteLine($"Product Price Current from Current Values {context.Entry(product).CurrentValues.GetValue<double>("PriceCurrent")}");

            WriteLine($"Product Price Current from Database Values {context.Entry(product).GetDatabaseValues().GetValue<double>("PriceCurrent")}");

            var state = context.Entry(product).State;

            context.Entry(product).Collection(p => p.ProductTagValues).Load();

            foreach (var tagValue in product.ProductTagValues)
            {
                if (tagValue.Id == 1)
                {
                    context.Entry(tagValue).State = EntityState.Modified;
                }

                WriteLine($"State for tag value with id {tagValue.Id} is {context.Entry(tagValue).State}");
            }

            WriteLine("=========================");

            WriteLine($"Product State: {context.Entry(product).State}");

            context.SaveChanges();

            Read();
        }
    }
}
