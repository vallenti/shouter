namespace SimpleMVC.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoutId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shouts", t => t.ShoutId, cascadeDelete: true)
                .Index(t => t.ShoutId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ShoutId", "dbo.Shouts");
            DropIndex("dbo.Notifications", new[] { "ShoutId" });
            DropTable("dbo.Notifications");
        }
    }
}
