using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WS.Database.Domain.Availability;
using WS.Database.Domain.Base;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Company;

namespace WS.Database.Domain.Products
{
    public class Product : Entity, ITenantEntity
    {
        #region Props

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Total number of items, possibly calcualted via the Availability Metric relations or set manually if no AMTemplate is
        /// available
        /// </summary>
        public int Stock { get; set; }

        public double PriceRegular { get; set; }

        public double? PriceCurrent { get; set; }

        public long ViewCount { get; set; }

        public bool IsOnSale { get; set; }

        public bool IsFeatured { get; set; }
        
        public bool IsTopSeller { get; set; }

        #endregion

        #region Rel Core

        public virtual Tenant Tenant { get; set; }

        /// <summary>
        /// The if od the category relational object.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Product always has category
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// The id of the manufacturer relational object
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Product should have a manufacturer
        /// </summary>
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual List<ProductImage> ProductImages { get; set; }

        #endregion

        #region Rel Tags

        public virtual List<ProductTagValue> ProductTagValues { get; set; }

        #endregion

        #region Availability Metrics

        public virtual AvailabilityMetricTemplate AvailabilityMetricTemplate { get; set; }

        public virtual List<ProductMetricValue> ProductMetricValues { get; set; }

        #endregion
    }
}
