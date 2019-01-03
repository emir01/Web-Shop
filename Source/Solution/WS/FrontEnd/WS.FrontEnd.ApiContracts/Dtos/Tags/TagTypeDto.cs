namespace WS.Contracts.Contracts.Dtos.Tags
{
    public class TagTypeDto:BaseDto
    {
        /// <summary>
        /// The name of the tag describing a given category
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The id of the category for which the tag is retrieved. This is not the category id to which
        /// the tag is linked if IsOwnTag is set to false.
        /// </summary>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Indicate if the tag is set directly on the category or is inherited from parent
        /// </summary>
        public bool IsOwnTag { get; set; }
        
        /// <summary>
        /// The name of the parent category to which this tag belongs to.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
