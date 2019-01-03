using System.Collections.Generic;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;
using WS.Database.Domain.Products;

namespace WS.Database.Domain.Availability
{
    /// <summary>
    /// Defines a named hirearchy of metrics for storing information
    /// about product availability.
    /// </summary>
    public class AvailabilityMetricTemplate:Entity
    {
        #region Props

        public string Name { get; set; }

        #endregion

        #region Rel

        public virtual List<Product> Products { get; set; }

        public virtual List<AvailabilityMetricUnit> AvailabilityMetricUnits { get; set; }

        #endregion
    }
}
