namespace WS.Database.Bootstrap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SetUserAsIsClientEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsClient", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsClient");
        }
    }
}
