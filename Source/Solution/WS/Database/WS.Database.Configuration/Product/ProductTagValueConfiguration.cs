using WS.Database.Domain.Products;

namespace WS.Database.Configuration.Product
{
    public class ProductTagValueConfiguration : BaseWsEntityTypeConfiguration<ProductTagValue>
    {
        public ProductTagValueConfiguration()
        {
            HasRequired(t => t.TagType)
                .WithMany()
                .WillCascadeOnDelete(true);
        }
    }
}
