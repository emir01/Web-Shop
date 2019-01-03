using AutoMapper;
using WS.Contracts.Contracts.Dtos.Images;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Core;
using WS.Database.Domain.Products;
using WS.Logic.Products.Objects;

namespace WS.Logic.Products.Mappings
{
    public static class ProductLogicAutoMapperConfig
    {
        public static void Map()
        {

            SetupCategoryMap();

            SetupManufacturerMap();

            SetupProductOperationObjectMap();

            SetupProductEditObjectMap();

            SetupProductPropertiesMap();

            SetupProductImageDtosMap();
        }

        private static void SetupProductImageDtosMap()
        {
            Mapper.CreateMap<ProductImageDto, ProductImage>();
            Mapper.CreateMap<AppImageDto, AppImage>();

            Mapper.CreateMap<ProductImage, ProductImageDto>();
            Mapper.CreateMap<AppImage, AppImageDto>();
        }

        private static void SetupProductEditObjectMap()
        {
            Mapper.CreateMap<ProductEditObject, Product>()
                .ForMember(prod => prod.CategoryId, opts => opts.MapFrom(pco => pco.Category.Id))
                .ForMember(prod => prod.Category, opts => opts.MapFrom(pco => pco.Category))
                .ForMember(prod => prod.Manufacturer, opts => opts.MapFrom(pco => pco.Manufacturer))
                .ForMember(prod => prod.ManufacturerId, opts => opts.MapFrom(pco => pco.Manufacturer.Id))
                .ForMember(prod => prod.ProductTagValues, opts => opts.MapFrom(pco => pco.Properties));
        }

        private static void SetupProductPropertiesMap()
        {
            Mapper.CreateMap<ProductTagValue, ProductProperty>()
                .ForMember(pp => pp.Name, opts => opts.MapFrom(ptv => ptv.TagType.Name))
                .ForMember(pp => pp.PropertyTypeId, opts => opts.MapFrom(ptv => ptv.TagTypeId));

            Mapper.CreateMap<ProductProperty, ProductTagValue>()
                .ForMember(ptv => ptv.TagTypeId, opts => opts.MapFrom(pp => pp.PropertyTypeId));
        }

        private static void SetupProductOperationObjectMap()
        {
            Mapper.CreateMap<Product, ProductOperationObject>();

            Mapper.CreateMap<ProductOperationObject, Product>()
                .ForMember(product => product.CategoryId, opts => opts.MapFrom(poo => poo.Category.Id))
                .ForMember(product => product.Category, opts => opts.UseValue(null))
                .ForMember(product => product.ManufacturerId, opts => opts.MapFrom(poo => poo.Manufacturer.Id))
                .ForMember(product => product.Manufacturer, opts => opts.UseValue(null));

            Mapper.CreateMap<Product, ProductEditObject>()
                .ForMember(pco => pco.Category, opts => opts.MapFrom(p => p.Category))
                .ForMember(pco => pco.Manufacturer, opts => opts.MapFrom(p => p.Manufacturer));
        }

        private static void SetupManufacturerMap()  
        {
            // base manufacturer logic operation mappings
            Mapper.CreateMap<Manufacturer, ManufacturerOperationObject>();
            Mapper.CreateMap<ManufacturerOperationObject, Manufacturer>();
        }

        private static void SetupCategoryMap()
        {
            // base category logic operation mappings
            Mapper.CreateMap<Category, CategoryOperationObject>()
                .ForMember(cop => cop.CategoryImage, opts => opts.MapFrom(c => c.CategoryImage.Uri));
            
            Mapper.CreateMap<CategoryOperationObject, Category>().ForMember(c => c.CategoryImage, opts => opts.Ignore());
        }
    }
}
