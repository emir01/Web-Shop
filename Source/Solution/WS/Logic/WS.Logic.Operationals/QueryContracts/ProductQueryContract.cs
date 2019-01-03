using System.Collections.Generic;

namespace WS.Logic.Core.QueryContracts
{
    /// <summary>
    /// Describe a base query contract for retrieving/searching products.
    /// </summary>
    public class ProductQueryContract : BaseQueryContarct
    {
        public ProductQueryContract()
        {
            TagFilters = new List<TagFilterContract>();
        }

        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int? ManufacturerId { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }
        
        public bool OnSale { get; set; }

        public bool IgnoreStatus { get; set; }
        
        public SortOptions Sort { get; set; }

        public List<TagFilterContract> TagFilters { get; set; }
    }
}
