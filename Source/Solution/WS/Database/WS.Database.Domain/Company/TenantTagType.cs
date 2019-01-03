using WS.Database.Domain.Base;
using WS.Database.Domain.Tagging;

namespace WS.Database.Domain.Company
{
    public class TenantTagType:Entity , ITenantEntity
    {
        #region Props

        public bool IsTenantOwner { get; set; }

        #endregion

        #region Rel

        public virtual TagType TagType { get; set; }

        public virtual Tenant Tenant { get; set; }

        #endregion
    }
}
