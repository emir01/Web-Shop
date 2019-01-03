using WS.Contracts.Contracts.Dtos.Manufacturers;
using WS.Database.Domain.Categorization;

namespace WS.FrontEnd.WebApi.Mappings.Modules
{
    public static class ManufacturerModuleMappings
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<Manufacturer, ManufacturerDto>();
            AutoMapper.Mapper.CreateMap<ManufacturerDto, Manufacturer>();
        }
    }
}