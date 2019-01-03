using System.Collections.Generic;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Categorization
{
    public class Manufacturer:Entity
    {
        #region Props

        public string Name { get; set; }

        #endregion

        #region Rel

        /// <summary>
        /// Each manufacturer defined in the system can belong to multiple product categories.
        /// </summary>
        public virtual List<Category> Categories { get; set; }

        #endregion

    }
}
