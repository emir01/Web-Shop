using System.Linq;
using WS.Core.Toolbox.Reflection;
using WS.Database.Domain.Base;

namespace WS.Database.Bootstrap.Seeding.SeededEntityWrapper
{
    /// <summary>
    /// A wrapper around seeded entities that allows for extension of the entities.
    /// 
    /// Used in concuction with the Seed Context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SeededEntityWrapper<T> where T : Entity, new()
    {
        #region Propes

        public T Data { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Builds the entitiy wrapper around a given entity.
        /// </summary>
        /// <param name="entity"></param>
        public SeededEntityWrapper(T entity)
        {
            Data = entity;
        }

        #endregion

        #region Extensions

        /// <summary>
        /// Extend the entity with an extension object, excluding the entity properties
        /// </summary>
        /// <param name="extension"></param>
        public SeededEntityWrapper<T> ExtendWith(T extension)
        {
            var entityProperties = typeof(Entity).GetProperties();

            TypeReflectionToolbox.CopyPublicProperties(extension, Data, entityProperties.ToList());

            return this;
        }

        #endregion
    }
}
