using System.Collections.Generic;
using WS.Contracts.Contracts.Dtos.Images;

namespace WS.Contracts.Contracts.Dtos.Categories
{
    public class CategorySimpleDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }
        
        public int Depth { get; set; }
    }

    public class CategoryHirearchyDto : BaseDto
    {
        /* 
            The properties are copied from Simple DTO because of issues with   AutoMapper not
            registering properly.
        */
        public string Name { get; set; }

        public string Description { get; set; }

        public AppImageDto CategoryImage { get; set; }

        public List<CategoryHirearchyDto> ChildCategories { get; set; }
    }
}
