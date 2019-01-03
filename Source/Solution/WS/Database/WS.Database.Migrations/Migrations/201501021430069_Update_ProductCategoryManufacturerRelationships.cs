namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ProductCategoryManufacturerRelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Categories", "IX_Manufacturer_Id");
            CreateTable(
                "dbo.CategoryManufacturers",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.ManufacturerId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ManufacturerId);
            
            AddColumn("dbo.Products", "ManufacturerId", c => c.Int());
            CreateIndex("dbo.Products", "ManufacturerId", name: "IX_Manufacturer_Id");
            AddForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers", "Id");
            DropColumn("dbo.Categories", "ManufacturerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ManufacturerId", c => c.Int());
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.CategoryManufacturers", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.CategoryManufacturers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.CategoryManufacturers", new[] { "ManufacturerId" });
            DropIndex("dbo.CategoryManufacturers", new[] { "CategoryId" });
            DropIndex("dbo.Products", "IX_Manufacturer_Id");
            DropColumn("dbo.Products", "ManufacturerId");
            DropTable("dbo.CategoryManufacturers");
            CreateIndex("dbo.Categories", "ManufacturerId", name: "IX_Manufacturer_Id");
            AddForeignKey("dbo.Categories", "ManufacturerId", "dbo.Manufacturers", "Id");
        }
    }
}
