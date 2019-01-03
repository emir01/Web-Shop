using System.Collections.Specialized;

namespace WS.FrontEnd.WebApi.Infrastucture.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static string FileType(this NameValueCollection collection)
        {
            var typeString = collection["Type"];
            
            return typeString;
        }

        public static int ProductId(this NameValueCollection collection)
        {
            var productIdString = collection["ProductId"];

            int id;
            int.TryParse(productIdString, out id);

            return id;
        }

        public static int CategoryId(this NameValueCollection collection)
        {
            var productIdString = collection["CategoryId"];

            int id;
            int.TryParse(productIdString, out id);

            return id;
        }

        public static int EntityId(this NameValueCollection collection)
        {
            var productIdString = collection["Id"];

            int id;
            int.TryParse(productIdString, out id);

            return id;
        }
    }
}