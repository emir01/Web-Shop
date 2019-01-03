using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Globalization;
using WS.FrontEnd.WebApi.Models;

namespace WS.FrontEnd.WebApi.Infrastucture.Converters
{
    public class TagFilterConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            CultureInfo culture, object value)
        {
            if (value is string)
            {
                var json = value as string;
                var jObject = JsonConvert.DeserializeObject(json) as JObject;

                var result = new TagFilter();

                if (jObject != null)
                {
                    result.FilterValue = jObject.GetValue("FilterValue", StringComparison.InvariantCultureIgnoreCase).Value<string>();
                    result.TagTypeId = jObject.GetValue("TagTypeId", StringComparison.InvariantCultureIgnoreCase).Value<int>();
                }
                
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}