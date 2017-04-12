namespace SimpleMVC.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShoutExpiration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shouts", "ExpiresOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shouts", "ExpiresOn");
        }
    }
}
