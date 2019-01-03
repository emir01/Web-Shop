using System.Linq;
using WS.Database.Domain.Core;

namespace WS.Database.Access.Interface.Repositories
{
    public interface IAppImageRepository
    {
        /// <summary>
        /// Return a queryable collection
        /// </summary>
        /// <returns></returns>
        IQueryable<AppImage> QueryActiveImagesForType(string type);
    }
}
