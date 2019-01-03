using System.Collections.Generic;

namespace WS.Logic.Products.Objects
{
    public class CompareProperty
    {
        public string PropertyName { get; set; }

        public string PropertyId { get; set; }

        public string PropertyType { get; set; }

        public List<ComparePropertyValue> CompareValues { get; set; }

        public CompareProperty()
        {
            CompareValues = new List<ComparePropertyValue>();
        }

        public void AddValue(string entityId, string value)
        {
            CompareValues.Add(ComparePropertyValue.Get(entityId, value));
        }

        public static CompareProperty Get(string propertyName, string propertyId, string propertyType = "string")
        {
            return new CompareProperty
            {
                PropertyName = propertyName,
                PropertyId = propertyId,
                PropertyType = propertyType
            };
        }

        public static CompareProperty Get(string propertyName, string propertyType = "string")
        {
            return new CompareProperty
            {
                PropertyName = propertyName,
                PropertyId = null,
                PropertyType = propertyType
            };
        }
    }
}