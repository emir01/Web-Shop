using System.Collections.Generic;
using System.Linq;

namespace WS.Logic.Products.Objects
{
    public class EntitiesCompareObject<T>
    {
        public List<CompareProperty> CompareProperties { get; set; }
        
        public List<T> Entities { get; set; }
        
        public EntitiesCompareObject()
        {
            CompareProperties = new List<CompareProperty>();
            Entities = new List<T>();
        }

        public EntitiesCompareObject<T> AddCompareProperty(string propertyName, string propertyType = "string")
        {
            AddCompareProperty(propertyName, propertyName, propertyType);
            return this;
        }

        public EntitiesCompareObject<T> AddCompareProperty(string propertyName, string propertyId, string propertyType = "string")
        {
            CompareProperties.Add(CompareProperty.Get(propertyName, propertyId, propertyType));
            return this;
        }

        public EntitiesCompareObject<T> AddValue(string entityId, string propertyId, string value)
        {
            var compareProperty = CompareProperties.FirstOrDefault(p => p.PropertyId == propertyId);
            compareProperty?.AddValue(entityId, value);
            return this;
        }
    }
}
