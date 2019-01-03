using System;
using WS.Database.Domain.Base;
using WS.Database.Domain.Core;

namespace WS.Database.Domain.Company
{
    public class TenantUrlHistory : Entity, ITenantEntity
    {
        #region Props

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public bool Active { get; set; }

        #endregion

        #region Rel

        public Tenant Tenant { get; set; }

        public AppUrl Url { get; set; }

        #endregion
    }
}
