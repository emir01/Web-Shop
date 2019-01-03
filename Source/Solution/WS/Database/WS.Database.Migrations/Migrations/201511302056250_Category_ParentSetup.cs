namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category_ParentSetup : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_Parent_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_ParentId", newName: "IX_Parent_Id");
        }
    }
}
