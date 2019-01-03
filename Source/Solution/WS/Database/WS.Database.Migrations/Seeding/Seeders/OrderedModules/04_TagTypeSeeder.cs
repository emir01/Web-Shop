using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Bootstrap.Seeding.SeedingContext;
using WS.Database.Domain;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Tagging;

namespace WS.Database.Bootstrap.Seeding.Seeders.OrderedModules
{
    public class TagTypeSeeder : ISeeder
    {
        #region Props

        public int SeedOrder { get; set; }

        #endregion

        #region Ctor

        public TagTypeSeeder()
        {
            SeedOrder = 4;
        }

        #endregion

        public void SeedData(ISeedContext seedContext)
        {
            SeedBikeTagTypes(seedContext);
        }

        private void SeedBikeTagTypes(ISeedContext seedContext)
        {
            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeBikeFrame).ExtendWith(new TagType()
            {
                Name = "Frame",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeBikeFrontFork).ExtendWith(new TagType()
            {
                Name = "Front Fork",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeBikeShifters).ExtendWith(new TagType()
            {
                Name = "Shifters",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeFrontDerailleur).ExtendWith(new TagType()
            {
                Name = "Front Derailleur",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeRearDerailleur).ExtendWith(new TagType()
            {
                Name = "Rear Derailleur",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeCrankset).ExtendWith(new TagType()
            {
                Name = "Crankset",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeBottomBracket).ExtendWith(new TagType()
            {
                Name = "Bottom Bracket",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeChain).ExtendWith(new TagType()
            {
                Name = "Chain",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));

            seedContext.Add(seedContext.BuildWrappedObject<TagType>(SeedingStrings.TagTypeCassette).ExtendWith(new TagType()
            {
                Name = "Cassette",
                Categories = seedContext.GetObjectsForAlias<Category>(SeedingStrings.CategoryBks)
            }.TimeStampNew()));
        }
    }
}
