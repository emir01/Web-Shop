using System;

namespace WS.Contracts.Contracts.Dtos.Images
{
    public class ProductImageDto : BaseDto
    {
        public bool IsPrimary { get; set; }

        public DateTime DateUploaded { get; set; }

        public int ProductId { get; set; }

        public AppImageDto Image { get; set; }
    }
}
