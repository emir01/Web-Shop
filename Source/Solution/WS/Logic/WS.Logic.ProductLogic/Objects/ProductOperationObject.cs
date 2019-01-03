using System.Collections.Generic;

namespace WS.Logic.Products.Objects
{
    /// <summary>
    /// The operational DTO object for product operations for displaying/creating/editing products
    /// todo: Possibly move to conctracts?
    /// </summary>
    public class ProductOperationObject
    {
        public ProductOperationObject()
        {
            CategoryHirearchy = new List<CategoryOperationObject>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryOperationObject Category { get; set; }
    
        public List<CategoryOperationObject> CategoryHirearchy { get; set; }

        public int CategoryId { get; set; }

        public ManufacturerOperationObject Manufacturer { get; set; }

        public int ManufacturerId { get; set; }

        public string PriceRegular { get; set; }

        public string PriceCurrent { get; set; }
        
        public bool? Status { get; set; }
    }
}