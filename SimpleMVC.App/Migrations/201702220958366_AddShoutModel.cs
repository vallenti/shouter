namespace SimpleMVC.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShoutModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        AuthorId = c.Int(nullable: false),
                        PublishedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shouts", "AuthorId", "dbo.Users");
            DropIndex("dbo.Shouts", new[] { "AuthorId" });
            DropTable("dbo.Shouts");
        }
    }
}
