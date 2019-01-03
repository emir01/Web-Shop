using WS.Database.Domain.Availability;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Products
{
    public class ProductMetricValue : Entity
    {
        #region Props

        public string Value { get; set; }

        #endregion

        #region Rel

        public virtual Product Product { get; set; }

        public virtual AvailabilityMetricUnit AvailabilityMetricUnit { get; set; }

        #endregion
    }
}
