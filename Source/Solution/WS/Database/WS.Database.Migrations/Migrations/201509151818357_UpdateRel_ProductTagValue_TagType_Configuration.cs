namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRel_ProductTagValue_TagType_Configuration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductTagValues", "TagTypeId", "dbo.TagTypes");
            DropIndex("dbo.ProductTagValues", "IX_TagType_Id");
            AlterColumn("dbo.ProductTagValues", "TagTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductTagValues", "TagTypeId", name: "IX_TagType_Id");
            AddForeignKey("dbo.ProductTagValues", "TagTypeId", "dbo.TagTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductTagValues", "TagTypeId", "dbo.TagTypes");
            DropIndex("dbo.ProductTagValues", "IX_TagType_Id");
            AlterColumn("dbo.ProductTagValues", "TagTypeId", c => c.Int());
            CreateIndex("dbo.ProductTagValues", "TagTypeId", name: "IX_TagType_Id");
            AddForeignKey("dbo.ProductTagValues", "TagTypeId", "dbo.TagTypes", "Id");
        }
    }
}
