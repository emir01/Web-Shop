namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ProductImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", "IX_Product_Id");
            AlterColumn("dbo.ProductImages", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductImages", "ProductId");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            AlterColumn("dbo.ProductImages", "ProductId", c => c.Int());
            CreateIndex("dbo.ProductImages", "ProductId", name: "IX_Product_Id");
            AddForeignKey("dbo.ProductImages", "ProductId", "dbo.Products", "Id");
        }
    }
}
