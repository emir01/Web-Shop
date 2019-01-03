namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryImageId", c => c.Int());
            CreateIndex("dbo.Categories", "CategoryImageId");
            AddForeignKey("dbo.Categories", "CategoryImageId", "dbo.AppImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "CategoryImageId", "dbo.AppImages");
            DropIndex("dbo.Categories", new[] { "CategoryImageId" });
            DropColumn("dbo.Categories", "CategoryImageId");
        }
    }
}
