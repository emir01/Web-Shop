using System.Collections.Generic;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Company
{
    /// <summary>
    /// The business domain is a meta table describing the businesses a tenant is 
    /// engaged in.
    /// 
    /// It can be used to provide meta information and group categories, tags and manufacturers
    /// for multiple companies.
    /// 
    /// Eg. Allow a bike shop to re-use the tags/categories/manufacturers of previous shops
    /// as they would be in the same BikeShop Domain.
    /// </summary>
    public  class BusinessDomain:Entity
    {
        #region Props

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

        #region Rel

        public virtual List<Tenant> Tenants { get; set; }

        #endregion
    }
}
