using System.Linq;
using WS.Contracts.Contracts.Dtos.Products;
using WS.Database.Domain.Products;
using WS.FrontEnd.WebApi.Models;
using WS.Logic.Core.QueryContracts;

namespace WS.FrontEnd.WebApi.Mappings.Modules
{
    public static class ProductModuleMappings
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<Product, ProductDto>()
             .ForMember(pDto => pDto.Price, opts => opts.MapFrom(p => p.PriceCurrent.HasValue ? p.PriceCurrent : p.PriceRegular))
             .ForMember(pdto => pdto.ImageUrl, opts => opts.MapFrom(p => p.ProductImages.FirstOrDefault(img => img.IsPrimary).Image.Uri))
             .ForMember(pdto => pdto.CategoryImage, opts => opts.MapFrom(p => p.Category.CategoryImage == null ? null : p.Category.CategoryImage.Uri))
             .ForMember(pDto => pDto.Manufacturer, opts => opts.MapFrom(p => p.Manufacturer.Name))
             .ForMember(pDto => pDto.ManufacturerId, opts => opts.MapFrom(p => p.Manufacturer.Id))
             .ForMember(pDto => pDto.Category, opts => opts.MapFrom(p => p.Category.Name))
             .ForMember(pDto => pDto.CategoryId, opts => opts.MapFrom(p => p.Category.Id))
             .ForMember(pDto => pDto.State, opts => opts.MapFrom(p => p.Status.HasValue ? p.Status.Value ? 1 : 0 : 0));

            AutoMapper.Mapper.CreateMap<Product, ProductAdminDto>()
                .ForMember(pDto => pDto.State, opts => opts.MapFrom(p => p.Status.HasValue ? p.Status.Value ? 1 : 0 : 0))
                .ForMember(pDto => pDto.Manufacturer, opts => opts.MapFrom(p => p.Manufacturer.Name))
                .ForMember(pDto => pDto.Category, opts => opts.MapFrom(p => p.Category.Name))
                .ForMember(pDto => pDto.ManufacturerId, opts => opts.MapFrom(p => p.Manufacturer.Id))
                .ForMember(pDto => pDto.CategoryId, opts => opts.MapFrom(p => p.Category.Id));
            
            AutoMapper.Mapper.CreateMap<ProductQuery, ProductQueryContract>();
            AutoMapper.Mapper.CreateMap<TagFilter, TagFilterContract>();
        }
    }
}