using System.Collections.Generic;
using WS.Contracts.Contracts.Dtos.Images;

namespace WS.Logic.Products.Objects
{
    /// <summary>
    /// Inherits from the product operation object and includes the properties list for displaying/editing properties
    /// </summary>
    public class ProductEditObject : ProductOperationObject
    {
        public List<ProductProperty> Properties { get; set; }

        public List<ProductImageDto> ProductImages { get; set; }
}
}
