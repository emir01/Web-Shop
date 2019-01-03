using System.Linq;
using WS.Contracts.Contracts.Dtos.Categories;
using WS.Database.Domain.Categorization;

namespace WS.FrontEnd.WebApi.Mappings.Modules
{
    public static class CategoryModuleMappings
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<Category, CategoryHirearchyDto>()
                .ForMember(cdto => cdto.ChildCategories, opts => opts.MapFrom(c => c.Children))
                .ForMember(cdto => cdto.CategoryImage, opts => opts.MapFrom(c => c.CategoryImage));

            AutoMapper.Mapper.CreateMap<Category, CategorySimpleDto>();
            
            AutoMapper.Mapper.CreateMap<CategoryHirearchyDto, Category>()
                .ForMember(c => c.Children, opts => opts.MapFrom(dto => dto.ChildCategories))
                .ForMember(c => c.CategoryImage, opts => opts.MapFrom(dto => dto.CategoryImage));
        }
    }
}