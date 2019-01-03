namespace WS.Logic.Products.Objects
{
    public class ComparePropertyValue
    {
        public string EntityId { get; set; }

        public string Value { get; set; }

        public static ComparePropertyValue Get(string entityId, string value)
        {
            return new ComparePropertyValue
            {
                EntityId = entityId,
                Value = value
            };
        }
    }
}