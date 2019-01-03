using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.FrontEnd.WebApi.Infrastucture.Extensions
{
    public static class CollectionExtensions
    {
        public static List<string> GetServerMappedFiles(this IEnumerable<string> relativeFiles)
        {
            var serverMappedPaths = relativeFiles.Select(f => HttpContext.Current.Server.MapPath(f)).ToList();
            return serverMappedPaths;
        }
    }
}