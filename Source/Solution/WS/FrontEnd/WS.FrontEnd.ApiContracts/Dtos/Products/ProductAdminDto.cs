namespace WS.Contracts.Contracts.Dtos.Products
{
    /// <summary>
    /// Simple and basic DTO used to render quick product information on search results
    /// </summary>
    public class ProductAdminDto : BaseDto
    {
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public int ManufacturerId { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }
    }
}
