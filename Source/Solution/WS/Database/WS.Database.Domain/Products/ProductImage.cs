using System;
using System.ComponentModel.DataAnnotations.Schema;
using WS.Database.Domain.Base;
using WS.Database.Domain.Core;

namespace WS.Database.Domain.Products
{
    public class ProductImage : Entity, IHaveEntityState
    {
        #region Props

        public bool IsPrimary { get; set; }

        public DateTime? DateUploaded { get; set; }

        public int ProductId { get; set; }

        #endregion

        #region Rel

        public AppImage Image { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        #endregion

        [NotMapped]
        public WsEntityState State { get; set; }
    }
}