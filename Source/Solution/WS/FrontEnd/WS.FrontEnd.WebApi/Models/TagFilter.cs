using System.ComponentModel;
using WS.FrontEnd.WebApi.Infrastucture.Converters;

namespace WS.FrontEnd.WebApi.Models
{
    [TypeConverter(typeof(TagFilterConverter))]
    public class TagFilter
    {
        public int TagTypeId { get; set; }

        public string FilterValue { get; set; }
    }
}