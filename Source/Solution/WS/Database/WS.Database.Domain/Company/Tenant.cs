using System.Collections.Generic;
using WS.Database.Domain.Base;
using WS.Database.Domain.Core;

namespace WS.Database.Domain.Company
{
    public class Tenant:Entity, IClientEntity
    {
        #region Props
    
        public string Name { get; set; }

        public string Title { get; set; }

        public bool IsClient { get; set; }

        #endregion

        #region Core Rel

        /// <summary>
        /// The current logo image for the tenant
        /// </summary>
        public virtual AppImage Logo { get; set; }

        /// <summary>
        /// The Tenant URl Change history
        /// </summary>
        public virtual List<TenantUrlHistory> TenanUrlHistory { get; set; }

        /// <summary>
        /// The collection of users asigned to the Tenant/Company account.
        /// </summary>
        public virtual List<User> Users { get; set; }

        /// <summary>
        /// The collection of business domains for the tenant.s
        /// </summary>
        public virtual List<BusinessDomain> BusinessDomains { get; set; }

        public virtual List<Country> Countries { get; set; }

        #endregion

        #region Category Rel

        public virtual List<TenantCategory> Categories { get; set; }

        #endregion

        #region Manufacturer Rel

        public virtual List<TenantManufacturer> Manufacturers { get; set; }

        #endregion

        #region Tagtype Rel

        public virtual List<TenantTagType> TagTypes { get; set; }

        #endregion

        #region Availability Rel

        public virtual List<TenantMetricTemplate> MetricTemplates { get; set; }

        #endregion
    }
}
