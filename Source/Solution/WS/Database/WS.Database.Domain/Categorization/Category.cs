using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WS.Database.Domain.Base;
using WS.Database.Domain.Core;
using WS.Database.Domain.Tagging;

namespace WS.Database.Domain.Categorization
{
    public class Category : Entity, IHaveEntityState
    {
        public Category()
        {
            State = WsEntityState.NoChanges;
        }

        #region Props

        public string Name { get; set; }

        public virtual int? ParentId { get; set; }

        public virtual int? CategoryImageId { get; set; }

        public string Description { get; set; }

        #endregion

        #region Rel Core

        public virtual List<TagType> TagTypes { get; set; }

        /// <summary>
        /// A single category can have multiple manufacturers for that category.
        /// </summary>
        public virtual List<Manufacturer> Manufacturers { get; set; }

        #endregion

        #region Rel Hirearchy

        /*
         * Hirearch definition relationshipss
         */
        public virtual Category Parent { get; set; }

        public virtual List<Category> Children { get; set; }
        
        [ForeignKey("CategoryImageId")]
        public virtual AppImage CategoryImage { get; set; }

        #endregion

        #region State Internal

        [NotMapped]
        public WsEntityState State { get; set; }

        #endregion
    }
}
