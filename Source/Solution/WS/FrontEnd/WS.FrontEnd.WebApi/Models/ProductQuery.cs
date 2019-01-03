using System.Collections.Generic;
using WS.Logic.Core.QueryContracts;

namespace WS.FrontEnd.WebApi.Models
{
    /// <summary>
    /// Describe a base query contract for retrieving/searching products.
    /// </summary>

    public class ProductQuery : Query
    {
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int? ManufacturerId { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }
        
        public bool OnSale { get; set; }
        
        public SortOptions Sort { get; set; }
        
        public bool IgnoreStatus { get; set; }
        
        public List<TagFilter> TagFilters { get; set; }
    }
}