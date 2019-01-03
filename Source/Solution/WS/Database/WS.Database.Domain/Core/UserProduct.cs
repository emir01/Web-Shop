using System;
using WS.Database.Domain.Base;
using WS.Database.Domain.Products;

namespace WS.Database.Domain.Core
{
    public class UserProduct:Entity
    {
        #region Props

        public DateTime DateSaved { get; set; }

        public bool IsFavourite { get; set; }
        
        #endregion

        #region Rel

        public virtual User User { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}
