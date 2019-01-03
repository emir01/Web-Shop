using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Bootstrap.Seeding.SeedingContext;
using WS.Database.Domain.Categorization;

namespace WS.Database.Bootstrap.Seeding.Seeders.OrderedModules
{
    public class CategorySeeder : ISeeder
    {
        #region Props

        public int SeedOrder { get; set; }

        #endregion

        #region Ctor

        public CategorySeeder()
        {
            SeedOrder = 3;
        }

        #endregion

        public void SeedData(ISeedContext seedContext)
        {
            AddMainProductCategoryForAllTenants(seedContext);

            AddBikeCategories(seedContext);

            AddBikeEquipmentCategories(seedContext);

            AddBikeClothingCategories(seedContext);
        }

        #region Main Product Category

        private static void AddMainProductCategoryForAllTenants(ISeedContext seedContext)
        {
            // add the system defined main product category
            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CAT_Product)
                    .ExtendWith(new Category()
                    {
                        Name = "Product",
                        Parent = null,
                    }).TimeStampNew().SetSystemFlag());
        }

        #endregion

        #region Bike Categories
        
        public void AddBikeCategories(ISeedContext seedContext)
        {
            //TODO : Add parent id for all categories
            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBks)
                    .ExtendWith(new Category()
                    {
                        Name = "Bikes",
                        Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CAT_Product),
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBksCross)
                    .ExtendWith(new Category()
                    {
                        Name = "Cross Bikes",
                        Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBks),
                        Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerCube, SeedingStrings.ManufacturerGiant, SeedingStrings.ManufacturerGt, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerTrek)
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBksHybrid)
                    .ExtendWith(new Category()
                    {
                        Name = "Hybrid Bikes",
                        Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBks),
                        Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerCube, SeedingStrings.ManufacturerGiant, SeedingStrings.ManufacturerGt, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerTrek)
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBksMountain)
                    .ExtendWith(new Category()
                    {
                        Name = "Mountain Bikes",
                        Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBks),
                        Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerCube, SeedingStrings.ManufacturerGiant, SeedingStrings.ManufacturerGt, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerTrek)
                    }).TimeStampNew());


            seedContext.Add(
                seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBksRoad)
                    .ExtendWith(new Category()
                    {
                        Name = "Road Bikes",
                        Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBks),
                        Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerGiant, SeedingStrings.ManufacturerGt, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerTrek)
                    }).TimeStampNew());
        }

        #endregion

        #region Bike Equipment Categories

        public void AddBikeEquipmentCategories
            (ISeedContext seedContext)
        {
            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipment)
                  .ExtendWith(new Category()
                  {
                      Name = "Bike Equipment",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CAT_Product),
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentBackGearset)
                  .ExtendWith(new Category()
                  {
                      Name = "Back Gearsets",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerSram, SeedingStrings.ManufacturerShimano)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentFrontGearset)
                  .ExtendWith(new Category()
                  {
                      Name = "Front Gearsets",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerSram, SeedingStrings.ManufacturerShimano)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentForks)
                  .ExtendWith(new Category()
                  {
                      Name = "Forks",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerFox, SeedingStrings.ManufacturerRockShox, SeedingStrings.ManufacturerSuntour)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentInnerTubes)
                  .ExtendWith(new Category()
                  {
                      Name = "Inner Tubes",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerMichelin, SeedingStrings.ManufacturerContinental, SeedingStrings.ManufacturerBontager),
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentTires)
                  .ExtendWith(new Category()
                  {
                      Name = "Tires",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerMichelin, SeedingStrings.ManufacturerContinental, SeedingStrings.ManufacturerBontager),
                  }).TimeStampNew());

            seedContext.Add(
             seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeEquipmentWheels)
                 .ExtendWith(new Category()
                 {
                     Name = "Wheels",
                     Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeEquipment),
                     Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerShimano, SeedingStrings.ManufacturerBontager),
                 }).TimeStampNew());
        }

        #endregion

        #region Bike Clothing Categories

        public void AddBikeClothingCategories(ISeedContext seedContext)
        {
            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothing)
                  .ExtendWith(new Category()
                  {
                      Name = "Bike Clothing",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CAT_Product),
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothingBottoms)
                  .ExtendWith(new Category()
                  {
                      Name = "Biking Bottoms",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothing),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerForceCz)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothingTops)
                  .ExtendWith(new Category()
                  {
                      Name = "Biking Tops",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothing),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerForceCz)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothingShoes)
                  .ExtendWith(new Category()
                  {
                      Name = "Biking Shoes",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothing),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerBontager, SeedingStrings.ManufacturerShimano)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothingHelmets)
                  .ExtendWith(new Category()
                  {
                      Name = "Biking Helmets",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothing),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerForceCz, SeedingStrings.ManufacturerGiro)
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Category>(SeedingStrings.CategoryBikeClothingWindJackets)
                  .ExtendWith(new Category()
                  {
                      Name = "Biking Wind Jackets",
                      Parent = seedContext.GetObjectForAlias<Category>(SeedingStrings.CategoryBikeClothing),
                      Manufacturers = seedContext.GetObjectsForAlias<Manufacturer>(SeedingStrings.ManufacturerTrek, SeedingStrings.ManufacturerSpecialized, SeedingStrings.ManufacturerForceCz, SeedingStrings.ManufacturerGiro)
                  }).TimeStampNew());
        }

        #endregion
    }
}
