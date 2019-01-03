using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WS.Core.Toolbox.Reflection
{
    public static class TypeReflectionToolbox
    {
        /// <summary>
        /// Copies the public properties from the source to the destination, excluding the excluded properties
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The destination object</param>
        /// <param name="excludeProperties">Optional collection of propety info objects that will be excluded from the copy process</param>
        public static void CopyPublicProperties<T>(T source, T destination, List<PropertyInfo> excludeProperties = null)
        {
            var properties = typeof(T).GetProperties().ToList();

            if (excludeProperties != null)
            {
                properties = properties.Where(t => excludeProperties.All(ex => ex.Name != t.Name)).ToList();
            }

            foreach (var propertyInfo in properties)
            {
                // get the value from the source
                var value = propertyInfo.GetValue(source);

                // set the value on the destination
                propertyInfo.SetValue(destination, value);
            }
        }
    }
}
