using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using WS.Database.Bootstrap.Context;
using WS.Database.Bootstrap.Seeding.Core;
using WS.Database.Bootstrap.Seeding.Core.Interfaces;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;

namespace WS.Database.Bootstrap.Seeding.SeedingContext
{
    /// <summary>
    /// Single Dynamic seeder context for all entites in the system. Only place in the entire
    /// system that gets to call Save on the Context
    /// </summary>
    public class SeedContext : ISeedContext
    {
        #region Props

        /// <summary>
        ///  The seed context will expose the actual context for read only
        /// if manual changes are required.
        /// </summary>
        public DbContext Context { get; private set; }

        private readonly ISeedUtilityService _seedUtilityService;

        private readonly bool _runDispose;

        #endregion

        #region Ctors

        public SeedContext(WsContext context, ISeedUtilityService seedUtilityService = null, bool runDispose = true)
        {
            Context = context;

            _seedUtilityService = seedUtilityService ?? new SeedUtilityService();

            _runDispose = runDispose;
        }

        #endregion

        #region Add

        /// <summary>
        /// Add/Seed a simple entity to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public T Add<T>(T entity) where T : Entity, new()
        {

            // if there is not an entitiy with the same alias in the set and local set add it
            if (_seedUtilityService.IsEntityAliasUnique<T>(Context, entity.Alias))
            {
                var added = Context.Set<T>().Add(entity);
                return added;
            }
            else
            {
                Debug.WriteLine(string.Format("There is already an entity with the alias {0}", entity.Alias));
                return null;
            }
        }

        /// <summary>
        /// Add/Seed a collection of entities to the context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public List<T> Add<T>(List<T> entities) where T : Entity, new()
        {
            var list = new List<T>();

            foreach (var entity in entities)
            {
                var addedEntity = Add(entity);
                list.Add(addedEntity);
            }

            return list;
        }

        /// <summary>
        ///  Add/Seed a wrapped entity to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wrapper"></param>
        public T Add<T>(SeededEntityWrapper<T> wrapper) where T : Entity, new()
        {
            return Add(wrapper.Data);
        }

        /// <summary>
        /// Add/Seed a collection of wrapped entities to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wrappedEntities"></param>
        public List<T> Add<T>(List<SeededEntityWrapper<T>> wrappedEntities) where T : Entity, new()
        {
            var list = new List<T>();

            foreach (var seededEntityWrapper in wrappedEntities)
            {
                var addedEntity = Add(seededEntityWrapper);
                list.Add(addedEntity);
            }

            return list;
        }


        #endregion

        #region Object Queries

        /// <summary>
        /// Return an entity from the seeding context 
        /// for the given alias
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public T GetObjectForAlias<T>(string alias) where T : Entity
        {
            var set = Context.Set<T>();

            // try and find the entity in the regular set
            var entity = set.FirstOrDefault(t => t.Alias == alias);

            if (entity == null)
            {
                // try and get it from the local set
                entity = set.Local.FirstOrDefault(t => t.Alias == alias);

                if (entity == null)
                {
                    throw new ArgumentException(string.Format("There is no entity with the alias {0} in set {1}", alias,
                        typeof(T).Name));
                }
                else
                {
                    return entity;
                }
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// Return a collection of objects based on the alias values provided as arguments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public List<T> GetObjectsForAlias<T>(params string[] aliases) where T : Entity
        {
            var listOfEntities = new List<T>();

            foreach (var alias in aliases)
            {
                listOfEntities.Add(GetObjectForAlias<T>(alias));
            }

            return listOfEntities;
        }

        #endregion

        #region Tenant

        public Tenant Tenant()
        {
            // check if the context has the test tenant
            var tenant = Context.Set<Tenant>().FirstOrDefault(t => t.Alias == SeedingStrings.SystemTenantAlias);

            if (tenant == null)
            {
                // check local
                tenant = Context.Set<Tenant>().Local.FirstOrDefault(t => t.Alias == SeedingStrings.SystemTenantAlias);

                if (tenant == null)
                {
                    throw new NullReferenceException("Requested Seed Test Tenant, which is currently not seeded");
                }
                else
                {
                    return tenant;
                }
            }
            else
            {
                return tenant;
            }
        }

        #endregion

        #region Object Builder

        /// <summary>
        /// Returna base entity object from a specific type to be seeded in the context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias">Seeded data alias is a required value to keep unique data inserts</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T BuildBaseObject<T>(string alias, int id = 0) where T : Entity, new()
        {
            var entity = new T();

            if (id != 0)
            {
                entity.Id = 0;
            }

            entity.Alias = alias;

            return entity;
        }

        /// <summary>
        /// Return a wrapped  entity object that allows further extension and can be 
        /// directly added to the context which will cause it to be unwrapped first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias">Seeded data alias is a required value to keep unique data inserts</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public SeededEntityWrapper<T> BuildWrappedObject<T>(string alias, int id = 0) where T : Entity, new()
        {
            var baseObject = BuildBaseObject<T>(alias, id);
            return new SeededEntityWrapper<T>(baseObject);
        }

        #endregion

        #region Disposable

        public void Dispose()
        {
            Context.SaveChanges();

            if (_runDispose)
            {
                Context.Dispose();
            }
        }

        #endregion
    }
}
