using System;
using System.Collections;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;

namespace WS.Database.Domain
{
    /// <summary>
    /// A Meta information manager static utility for entitiy data
    /// </summary>
    public static class EntityMetaManager
    {
        #region Timestamps Dates

        public static T TimeStampNew<T>(this T entity, string createdBy = null) where T : Entity
        {
            entity.DateCreated = DateTime.Now;

            if (createdBy != null)
            {
                entity.CreatedBy = createdBy;
            }

            return entity;
        }

        public static T TimeStampUpdate<T>(this T entity, string updatedBy = null) where T : Entity
        {
            entity.DateModified = DateTime.Now;

            if (updatedBy != null)
            {
                entity.ModifiedBy = updatedBy;
            }

            return entity;
        }

        #endregion

        #region Flag Metadata

        public static T SetSystemFlag<T>(this T entity, bool isSystemFlag = true) where T : Entity
        {
            entity.IsSystem = isSystemFlag;
            return entity;
        }

        public static T SetStatusFlag<T>(this T entity, bool statusFlag = false) where T : Entity
        {
            entity.Status = statusFlag;
            return entity;
        }

        public static T SetClientFlag<T>(this T entity, bool clientFlag = false) where T : Entity, IClientEntity
        {
            entity.IsClient = clientFlag;
            return entity;
        }

        #endregion

        #region Tenant

        public static T TimeStampTenant<T>(this T entity, Tenant tenant = null) where T : Entity, ITenantEntity
        {
            if (tenant == null)
            {
                throw new NullReferenceException("Timestamp Null Tenant is not allowed");
            }

            entity.Tenant = tenant;

            return entity;
        }

        #endregion

        #region Alias

        /// <summary>
        /// Generate a random alias or base an alias from the given root. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="aliasRoot"></param>
        /// <returns></returns>
        public static T Alias<T>(this T entity, string aliasRoot = null) where T : Entity
        {
            if (aliasRoot != null)
            {
                // build the alias from the root so it will not be random
                var alias = aliasRoot.Replace(' ', '_');
                alias = alias.ToLower();
                entity.Alias = alias;
            }
            else
            {
                entity.Alias = "Alias_" + Guid.NewGuid();
            }

            return entity;
        }

        #endregion
    }
}
