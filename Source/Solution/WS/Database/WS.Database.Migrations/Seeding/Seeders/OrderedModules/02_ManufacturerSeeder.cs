using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Bootstrap.Seeding.SeedingContext;
using WS.Database.Domain.Categorization;

namespace WS.Database.Bootstrap.Seeding.Seeders.OrderedModules
{
    public class ManufacturerSeeder : ISeeder
    {
        #region Props

        public int SeedOrder { get; set; }

        #endregion

        #region Ctor

        public ManufacturerSeeder()
        {
            SeedOrder = 2;
        }

        #endregion

        public void SeedData(ISeedContext seedContext)
        {
            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerBontager)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Bontager",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerCube)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Cube",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerForceCz)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Force Cz",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerBiemme)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Biemme"
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerGiant)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Giant",
                    }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerGt)
                  .ExtendWith(new Manufacturer()
                  {
                      Name = "GT",
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerSpecialized)
                  .ExtendWith(new Manufacturer()
                  {
                      Name = "Specialized",
                  }).TimeStampNew());

            seedContext.Add(
              seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerTrek)
                  .ExtendWith(new Manufacturer()
                  {
                      Name = "Trek",
                  }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerShimano)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Shimano",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerSram)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Sram",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerSuntour)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Suntour",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerRockShox)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Rock Shox",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerFox)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Foxs",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerRockShox)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Fox",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerMichelin)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Michelin",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerContinental)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Continental",
                    }).TimeStampNew());

            seedContext.Add(
                seedContext.BuildWrappedObject<Manufacturer>(SeedingStrings.ManufacturerGiro)
                    .ExtendWith(new Manufacturer()
                    {
                        Name = "Giro",
                    }).TimeStampNew());
        }
    }
}
