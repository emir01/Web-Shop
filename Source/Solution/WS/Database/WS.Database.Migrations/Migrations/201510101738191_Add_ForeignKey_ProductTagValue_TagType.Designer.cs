// <auto-generated />
namespace WS.Database.Bootstrap.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.2-beta2-31111")]
    public sealed partial class Add_ForeignKey_ProductTagValue_TagType : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Add_ForeignKey_ProductTagValue_TagType));
        
        string IMigrationMetadata.Id
        {
            get { return "201510101738191_Add_ForeignKey_ProductTagValue_TagType"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
