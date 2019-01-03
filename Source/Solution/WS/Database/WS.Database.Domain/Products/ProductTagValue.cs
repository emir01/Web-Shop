using System.ComponentModel.DataAnnotations.Schema;
using WS.Database.Domain.Base;
using WS.Database.Domain.Tagging;

namespace WS.Database.Domain.Products
{
    public class ProductTagValue:Entity
    {
        #region Props
        
        public string Value { get; set; }

        /// <summary>
        /// The id of the tag type for which the product tag value belongs to.
        /// </summary>
        public int TagTypeId { get; set; }

        #endregion

        #region Rel
        
        [ForeignKey("TagTypeId")]
        public virtual TagType TagType { get; set; }

        public virtual Product Product { get; set; }

        #endregion
    }
}
