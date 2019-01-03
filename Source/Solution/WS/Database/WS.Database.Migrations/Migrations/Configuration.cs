using WS.Database.Bootstrap.Seeding;
using WS.Database.Bootstrap.Seeding.SeedingContext;

namespace WS.Database.Bootstrap.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.WsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(Context.WsContext context)
        {
            using (var seedContext = new SeedContext(context, runDispose: false))
            {
                SeedLoader.LoadSeeders(seedContext);
            }
        }
    }
}
