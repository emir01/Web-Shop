using System.Collections.Generic;
using System.Linq;
using WS.Contracts.Contracts.Dtos.Tags;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Tagging;

namespace WS.FrontEnd.WebApi.Mappings.Modules
{
    public static class TagModuleMappings
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<TagType, TagTypeDto>()
                .ForMember(dto => dto.CategoryName, opts => opts.MapFrom(tag => tag.Categories.FirstOrDefault().Name))
                .ForMember(dto => dto.CategoryId, opts => opts.MapFrom(tag => tag.Categories.FirstOrDefault().Id));

            AutoMapper.Mapper.CreateMap<TagTypeDto, TagType>()
                .ForMember(tag => tag.Categories, opts => opts.MapFrom(dto => new List<Category> { new Category { Name = dto.CategoryName, Id = dto.Id } }));
        }
    }
}