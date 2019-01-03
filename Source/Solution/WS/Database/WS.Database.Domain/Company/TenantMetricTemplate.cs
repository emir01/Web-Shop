using WS.Database.Domain.Availability;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Company
{
    public class TenantMetricTemplate:Entity, ITenantEntity
    {
        #region Propss

        public bool IsTenantOwner { get; set; }

        #endregion

        #region Rel

        public virtual Tenant Tenant { get; set; }

        public virtual AvailabilityMetricTemplate AvailabilityMetricTemplate { get; set; }

        #endregion
    }
}
