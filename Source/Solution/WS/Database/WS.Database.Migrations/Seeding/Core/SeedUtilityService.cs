using System.Data.Entity;
using System.Linq;
using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Domain.Base;

namespace WS.Database.Bootstrap.Seeding.Core
{
    public class SeedUtilityService
        : ISeedUtilityService
    {
        /// <summary>
        /// Check if the given alias for an entity of type T exists in the context 
        /// active or local set for that entity of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public bool IsEntityAliasUnique<T>(DbContext context, string alias) where T : Entity
        {
            // we are also going to be checking the local set for any any entities
            // during the seeding process
            var set = context.Set<T>();
            var localSet = context.Set<T>().Local;

            // if there is not an entitiy with the same alias in the set and local set add it
            return (!set.Any(t => t.Alias == alias) && (localSet != null && !localSet.Any(t => t.Alias == alias)));
        }
}
}