using WS.Database.Domain.Base;
using WS.Database.Domain.Categorization;

namespace WS.Database.Domain.Company
{
    public class TenantCategory:Entity, ITenantEntity
    {
        #region Props

        public bool IsTenantOwner { get; set; }

        #endregion

        #region Rel

        public virtual Tenant Tenant { get; set; }

        public virtual Category Category { get; set; }

        #endregion
    }
}
