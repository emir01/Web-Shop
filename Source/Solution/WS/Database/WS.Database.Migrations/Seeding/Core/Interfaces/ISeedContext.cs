using System;
using System.Collections.Generic;
using System.Data.Entity;
using WS.Database.Bootstrap.Seeding.SeededEntityWrapper;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;

namespace WS.Database.Bootstrap.Seeding.Core.Interfaces
{
    /// <summary>
    /// The interface describing the seeding context that wraps and provides
    /// generic disposable functionality for seeding the data using the Applications WsContext
    /// </summary>
    public interface ISeedContext : IDisposable
    {
        #region Props

        /// <summary>
        ///  The seed context will expose the actual context for read only
        /// if manual changes are required.
        /// </summary>
        DbContext Context { get; }

        #endregion

        #region Add

        /// <summary>
        /// Add/Seed a simple entity to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        T Add<T>(T entity) where T : Entity, new();

        /// <summary>
        /// Add/Seed a collection of entities to the context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        List<T> Add<T>(List<T> entities) where T : Entity, new();

        /// <summary>
        ///  Add/Seed a wrapped entity to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wrapper"></param>
        T Add<T>(SeededEntityWrapper<T> wrapper) where T : Entity, new();

        /// <summary>
        /// Add/Seed a collection of wrapped entities to the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wrappedEntities"></param>
        List<T> Add<T>(List<SeededEntityWrapper<T>> wrappedEntities) where T : Entity, new();

        #endregion

        #region Object Queries

        /// <summary>
        /// Return an entity from the seeding context 
        /// for the given alias
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        T GetObjectForAlias<T>(string alias) where T : Entity;

        /// <summary>
        /// Return a collection of objects based on the alias values provided as arguments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        List<T> GetObjectsForAlias<T>(params string[] alias) where T : Entity;

        #endregion

        #region Tenant

        Tenant Tenant();
        
        #endregion

        #region Object Building

        /// <summary>
        /// Returna base entity object from a specific type to be seeded in the context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias">Seeded data alias is a required value to keep unique data inserts</param>
        /// <param name="id"></param>
        /// <returns></returns>
        T BuildBaseObject<T>(string alias, int id = 0) where T : Entity, new();

        /// <summary>
        /// Return a wrapped  entity object that allows further extension and can be 
        /// directly added to the context which will cause it to be unwrapped first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias">Seeded data alias is a required value to keep unique data inserts</param>
        /// <param name="id"></param>
        /// <returns></returns>
        SeededEntityWrapper<T> BuildWrappedObject<T>(string alias, int id = 0) where T : Entity, new();

        #endregion
    }
}