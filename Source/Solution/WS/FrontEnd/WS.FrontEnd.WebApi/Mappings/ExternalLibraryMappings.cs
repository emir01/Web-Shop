using WS.Logic.Products.Mappings;

namespace WS.FrontEnd.WebApi.Mappings
{
    /// <summary>
    /// Responsible for managing and calling all external library mappings.
    /// </summary>
    public static class ExternalLibraryMappings
    {
        public static void ExecuteMappings()
        {
            ProductLogicAutoMapperConfig.Map();
        }
    }
}