using System.Collections.Generic;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Availability
{
    public class AvailabilityMetricUnit : Entity
    {
        #region Props

        public string Name { get; set; }

        #endregion

        #region Rel Core

        public virtual AvailabilityMetricTemplate AvailabilityMetricTemplate { get; set; }

        #endregion

        #region Rel Hirearchy

        public virtual AvailabilityMetricUnit Parent { get; set; }

        public virtual List<AvailabilityMetricUnit> Children { get; set; }

        #endregion
    }
}
