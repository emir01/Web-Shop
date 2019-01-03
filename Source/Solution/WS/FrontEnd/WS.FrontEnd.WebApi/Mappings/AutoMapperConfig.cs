using WS.FrontEnd.WebApi.Mappings.Modules;

namespace WS.FrontEnd.WebApi.Mappings
{
    /// <summary>
    /// The main entry point for auto mapper configuration. 
    /// Call modules for web api object mappings and delegates work to the external library mapping object.
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            GeneralMappings.Map();
            
            ProductModuleMappings.Map();
            
            CategoryModuleMappings.Map();

            TagModuleMappings.Map();

            ManufacturerModuleMappings.Map();
            
            ExternalLibraryMappings.ExecuteMappings();
        }
    }
}