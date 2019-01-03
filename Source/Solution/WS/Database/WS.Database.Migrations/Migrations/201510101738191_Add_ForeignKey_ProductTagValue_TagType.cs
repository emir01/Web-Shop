namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ForeignKey_ProductTagValue_TagType : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.ProductTagValues", name: "IX_TagType_Id", newName: "IX_TagTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductTagValues", name: "IX_TagTypeId", newName: "IX_TagType_Id");
        }
    }
}
