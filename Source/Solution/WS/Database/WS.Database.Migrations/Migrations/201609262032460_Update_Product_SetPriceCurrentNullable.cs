namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_SetPriceCurrentNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PriceCurrent", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PriceCurrent", c => c.Double(nullable: false));
        }
    }
}
