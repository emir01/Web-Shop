using System.Data.Entity;
using WS.Database.Bootstrap.Context;
using WS.Database.Domain.Base;

namespace WS.Database.Bootstrap.Seeding.Core.Interfaces
{
    /// <summary>
    /// Contains a collection of utility services and methods
    /// in regards to data seeding used in the seed context.
    /// 
    /// Mainly used to allow better testabiliy.
    /// </summary>
    public interface ISeedUtilityService
    {
        /// <summary>
        /// Check if the given alias for an entity of type T exists in the context 
        /// active or local set for that entity of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        bool IsEntityAliasUnique<T>(DbContext context, string alias) where T : Entity;
    }
}
