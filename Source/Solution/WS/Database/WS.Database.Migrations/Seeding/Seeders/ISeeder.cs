using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeedingContext;

namespace WS.Database.Bootstrap.Seeding.Seeders
{
    public interface ISeeder
    {
        int SeedOrder { get; set; }

        void SeedData(ISeedContext seedContext);
    }
}