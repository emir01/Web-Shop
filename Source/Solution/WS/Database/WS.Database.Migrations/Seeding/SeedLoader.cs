using System;
using System.Linq;
using System.Reflection;
using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.Seeders;
using WS.Database.Bootstrap.Seeding.SeedingContext;

namespace WS.Database.Bootstrap.Seeding
{
    /// <summary>
    /// A utility seeding module that loads up all the defined ISeeders and injects
    /// the data via the Seeding Context.
    /// </summary>
    public static class SeedLoader
    {
        public static void LoadSeeders(ISeedContext seedContext)
        {
            // invoke seeders using the seed context on subdomains of the entire 
            // business domain

            var loaderType = typeof(ISeeder);

            var domainSeeders = Assembly.GetAssembly(typeof(SeedLoader)).GetTypes()
                .Where(type => loaderType.IsAssignableFrom(type) && !type.IsInterface);

            // we are going to need a seeder list to order the seeders by seed order
            var seederList = domainSeeders.Select(domainSeeder => (ISeeder)Activator.CreateInstance(domainSeeder)).ToList().OrderBy(s => s.SeedOrder);

            foreach (var seeder in seederList)
            {
                seeder.SeedData(seedContext);
            }
        }
    }
}
