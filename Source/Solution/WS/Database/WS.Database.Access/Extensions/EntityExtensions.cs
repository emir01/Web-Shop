using System.Linq;
using WS.Database.Domain.Base;

namespace WS.Database.Access.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsNew(this Entity entity)
        {
            bool idIsZero = entity.Id == 0;

            var entityAsStateEntity = entity as IHaveEntityState;

            if (entityAsStateEntity != null)
            {
                return idIsZero && entityAsStateEntity.State != WsEntityState.Deleted;
            }

            return idIsZero;
        }

        public static bool IsModified(this Entity entity)
        {
            bool idNotzero = entity.Id != 0 && entity.Id > 0;

            var entityAsStateEntity = entity as IHaveEntityState;

            if (entityAsStateEntity != null)
            {
                return idNotzero && entityAsStateEntity.State != WsEntityState.Deleted;
            }

            return idNotzero;
        }

        public static bool IsMarkedDeleted(this IHaveEntityState entityWithState)
        {
            return entityWithState.State == WsEntityState.Deleted;
        }

        public static IQueryable<T> OnlyActive<T>(this IQueryable<T> entities, bool ignoreStatus = false) where T : Entity
        {
            return entities.Where(x => (x.Status.HasValue && x.Status.Value) || ignoreStatus);
        }
    }
}
