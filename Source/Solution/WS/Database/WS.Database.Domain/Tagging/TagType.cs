using System.Collections.Generic;
using WS.Database.Domain.Base;
using WS.Database.Domain.Categorization;

namespace WS.Database.Domain.Tagging
{
    public class TagType:Entity
    {
        #region Props

        public string Name { get; set; }

        #endregion

        #region Rel
        
        public virtual List<Category> Categories { get; set; }

        #endregion
    }
}
