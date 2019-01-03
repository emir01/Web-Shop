using WS.FrontEnd.WebApi.Models;
using WS.Logic.Core.QueryContracts;

namespace WS.FrontEnd.WebApi.Mappings.Modules
{
    public static class GeneralMappings
    {
        public static void Map()
        {
            AutoMapper.Mapper.CreateMap<Query, BaseQueryContarct>();
        }
    }
}