using System.Collections.Generic;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;

namespace WS.Database.Domain.Core
{
    public class User : Entity, ITenantEntity, IClientEntity
    {
        #region Props

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsClient { get; set; }

        #endregion

        #region Rel

        /// <summary>
        /// Users can optionally bellong to a given tenant.
        /// </summary>
        public virtual Tenant Tenant { get; set; }

        /// <summary>
        /// To which country does the user belong to.
        /// </summary>
        public virtual Country Country { get; set; }

        public virtual List<UserProduct> UserProducts { get; set; }

        #endregion
    }
}
