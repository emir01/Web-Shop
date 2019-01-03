namespace WS.Logic.Products.Objects
{
    public class ProductProperty
    {
        /// <summary>
        /// The id for the Product Tag Value
        /// </summary>
        public int Id { get; set; }
        
        public int PropertyTypeId { get; set; }
        
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
