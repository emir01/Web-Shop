using WS.Database.Domain;
using WS.Database.Domain.Base;
using WS.Database.Domain.Company;

namespace WS.Database.Bootstrap.Seeding.SeededEntityWrapper
{
    /// <summary>
    /// Wraps/Modifies the Meta Manager extension methods to work with Wrapped Entity Objects
    /// </summary>
    public static class EntityWrapperMetaManagerExtensions
    {
        #region Timestamp Dates

        public static SeededEntityWrapper<T> TimeStampNew<T>(this SeededEntityWrapper<T> wrappedEntity,
            string createdBy = null) where T : Entity, new()
        {
            wrappedEntity.Data.TimeStampNew(createdBy);
            return wrappedEntity;
        }

        public static SeededEntityWrapper<T> TimeStampUpdate<T>(this SeededEntityWrapper<T> wrappedEntity,
            string updatedBy = null) where T : Entity, new()
        {
            wrappedEntity.Data.TimeStampUpdate(updatedBy);
            return wrappedEntity;
        }

        #endregion

        #region Flag Metadata

        public static SeededEntityWrapper<T> SetSystemFlag<T>(this SeededEntityWrapper<T> input,
            bool isSystemFlag = true) where T : Entity, new()
        {
            input.Data.IsSystem = isSystemFlag;
            return input;
        }

        public static SeededEntityWrapper<T> SetStatusFlag<T>(this SeededEntityWrapper<T> input, bool statusFlag = false)
            where T : Entity, new()
        {
            input.Data.Status = statusFlag;
            return input;
        }

        public static SeededEntityWrapper<T> SetClientFlag<T>(this SeededEntityWrapper<T> input, bool clientFlag = false)
            where T : Entity, IClientEntity, new()
        {
            input.Data.IsClient = clientFlag;
            return input;
        }

        #endregion

        #region Tenant

        public static SeededEntityWrapper<T> TimeStampTenant<T>(this SeededEntityWrapper<T> wrappedEntity,
            Tenant tenant = null) where T : Entity, ITenantEntity, new()
        {
            wrappedEntity.Data.TimeStampTenant(tenant);
            return wrappedEntity;
        }

        #endregion

        #region Alias

        public static SeededEntityWrapper<T> RandAlias<T>(this SeededEntityWrapper<T> wrappedEntity,
          string defAlias = null) where T : Entity, ITenantEntity, new()
        {
            wrappedEntity.Data.Alias(defAlias);
            return wrappedEntity;
        }

        #endregion
    }
}
