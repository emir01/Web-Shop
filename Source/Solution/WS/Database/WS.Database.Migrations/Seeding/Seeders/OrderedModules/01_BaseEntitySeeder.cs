using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Bootstrap.Seeding.SeedingContext;
using WS.Database.Domain;
using WS.Database.Domain.Company;
using WS.Database.Domain.Core;

namespace WS.Database.Bootstrap.Seeding.Seeders.OrderedModules
{
    public class BaseEntitySeeder : ISeeder
    {
        #region Properties

        public int SeedOrder { get; set; }

        #endregion

        #region Ctor

        public BaseEntitySeeder()
        {
            SeedOrder = 1;
        }

        #endregion

        #region Seeder Interface

        public void SeedData(ISeedContext seedContext)
        {
            SeedBusinessDomains(seedContext);

            SeedMainTenant(seedContext);

            SeedMainUser(seedContext);
        }

        #endregion

        #region Private Utilities

        private void SeedBusinessDomains(ISeedContext seedContext)
        {
            seedContext.Add(seedContext.BuildWrappedObject<BusinessDomain>(SeedingStrings.BusinessDomainBikeStore).ExtendWith(new BusinessDomain()
            {
                Name = "Bike Store"
            }).TimeStampNew().SetSystemFlag());

            seedContext.Add(seedContext.BuildWrappedObject<BusinessDomain>(SeedingStrings.BusinessDomainBikeEquipmentStore).ExtendWith(new BusinessDomain()
            {
                Name = "Bike Equipment Store"
            }).TimeStampNew().SetSystemFlag());

            seedContext.Add(seedContext.BuildWrappedObject<BusinessDomain>(SeedingStrings.BusinessDomainBikeClothingStore).ExtendWith(new BusinessDomain()
            {
                Name = "Bike Clothing Store"
            }).TimeStampNew().SetSystemFlag());

            // link business domains with entity
        }

        private void SeedMainTenant(ISeedContext seedContext)
        {
            seedContext.Add(seedContext.BuildWrappedObject<Tenant>(SeedingStrings.SystemTenantAlias).ExtendWith(new Tenant
            {
                Name = "WebShop",

                BusinessDomains = seedContext.GetObjectsForAlias<BusinessDomain>(SeedingStrings.BusinessDomainBikeClothingStore,
                SeedingStrings.BusinessDomainBikeEquipmentStore,
                SeedingStrings.BusinessDomainBikeStore)

            }).TimeStampNew().SetSystemFlag().SetClientFlag());
        }

        private void SeedMainUser(ISeedContext seedContext)
        {
            seedContext.Add(seedContext.BuildWrappedObject<User>(SeedingStrings.SystemUser).ExtendWith(new User()
            {
                Username = "admin",
                Password = "admin"
            }).TimeStampNew().SetSystemFlag().SetClientFlag().TimeStampTenant(seedContext.Tenant()));
        }

        #endregion
    }
}
