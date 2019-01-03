using WS.Database.Domain.Categorization;

namespace WS.Database.Configuration.Categorization
{
    public class CategoryConfiguration : BaseWsEntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            HasMany(cat => cat.TagTypes)
                 .WithMany(tg => tg.Categories)
                 .Map(mc =>
                 {
                     mc.MapLeftKey("CategoryId");
                     mc.MapRightKey("TagTypeId");
                     mc.ToTable("CategoryTagTypes");
                 });

            HasMany(cat => cat.Manufacturers)
             .WithMany(tg => tg.Categories)
             .Map(mc =>
             {
                 mc.MapLeftKey("CategoryId");
                 mc.MapRightKey("ManufacturerId");
                 mc.ToTable("CategoryManufacturers");
             });
            
            HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId).WillCascadeOnDelete(false);
        }
    }
}
