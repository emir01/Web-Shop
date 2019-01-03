using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WS.Database.Domain;
using WS.Database.Domain.Base;

namespace WS.Database.Access.Core
{
    public abstract class GenericRepository<T, TContext>
        where T : Entity
        where TContext : DbContext
    {
        #region Properties

        protected readonly TContext Context;

        protected readonly DbSet<T> Set;

        #endregion

        #region Ctor

        protected GenericRepository(TContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        #endregion

        #region Create

        public T CreateEntity(T entity)
        {
            StampNew(entity);

            Set.Add(entity);

            return entity;
        }

        #endregion

        #region Read

        public T GetEntityById(object id)
        {
            var entity = Set.Find(id);

            return entity;
        }

        public T GetEntityByAlias(string alias)
        {
            var entity = Set.FirstOrDefault(ent => ent.Alias == alias);
            return entity;
        }

        public IEnumerable<T> QueryEntities()
        {
            return Set;
        }

        public IEnumerable<T> QueryEntitiesByExpression(Expression<Func<T, bool>> query)
        {
            var result = Set.Where(query);

            return result;
        }

        #endregion

        #region Update

        /// <summary>
        /// Attach the given entity to the set and reset the modified configuration
        /// on the metadata entitiy properties
        /// </summary>
        /// <param name="entity"></param>
        public void AttachAndModify(T entity)
        {
            if (!IsAtached(entity))
            {
                Set.Attach(entity);
            }
            
            Context.Entry(entity).State = EntityState.Modified;

            ResetModifiedStateOnMetaProperties(entity);

            StampUpdate(entity);
        }

        #endregion

        #region Delete

        public T DeleteEntity(T entity)
        {
            Set.Remove(entity);
            return entity;
        }

        public void SetCategoryStatesAsDeleted<TEntity>(IEnumerable<TEntity> collection) where TEntity : Entity
        {
            foreach (var item in collection)
            {
                Context.Entry(item).State = item.Id != 0 ? EntityState.Deleted : EntityState.Detached;
            }
        }

        public void SetStateUnchanged<TEntity>(List<TEntity> collection) where TEntity : Entity
        {
            if (collection != null)
            {
                foreach (var entity in collection)
                {
                    Context.Entry(entity).State = EntityState.Unchanged;
                }
            }
        }

        #endregion

        #region Entity Status

        public T DisableEntity(T entity)
        {
            entity.Status = false;
            return entity;
        }

        public T EnableEntity(T entity)
        {
            entity.Status = true;
            return entity;
        }

        #endregion

        #region Stamps

        public void StampNew(T entity)
        {
            entity.TimeStampNew();
        }

        public void StampNew<TEntity>(TEntity entity) where TEntity : Entity
        {
            entity.TimeStampNew();
        }

        public void StampUpdate(T entity)
        {
            entity.TimeStampUpdate();
        }

        public void StampUpdate<TEntity>(TEntity entity) where TEntity : Entity
        {
            ResetModifiedStateOnMetaProperties(entity);
            entity.TimeStampUpdate();
        }

        #endregion

        #region Entity Utilities

        /// <summary>
        /// Clear the meta properties from the modified state when running updates on entities.
        /// </summary>
        /// <param name="entity"></param>
        public void ResetModifiedStateOnMetaProperties<TEntity>(TEntity entity) where TEntity : Entity
        {
            var baseEntityProperties = typeof(Entity).GetProperties().Select(p => p.Name).Where(v => v != "Id");

            foreach (var baseEntityPropertyName in baseEntityProperties)
            {
                Context.Entry(entity).Property(baseEntityPropertyName).IsModified = false;
            }
        }

        public bool IsAtached(T entity)
        {
            return Set.Local.Any(e => e == entity);
        }

        #endregion
    }
}
