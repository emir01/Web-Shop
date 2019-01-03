using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Interface.Repositories;
using WS.Database.Domain.Core;

namespace WS.Database.Access.Core.Repos
{
    public class AppImageRepository : GenericRepository<AppImage, DbContext>, IAppImageRepository
    {
        public AppImageRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Return a queryable collection
        /// </summary>
        /// <returns></returns>
        public IQueryable<AppImage> QueryActiveImagesForType(string type)
        {
            return
                Set.Where(appImage => appImage.Status.HasValue && appImage.Status.Value)
                    .Where(appimage => appimage.Type.ToLower() == type.ToLower());
        }
    }
}
