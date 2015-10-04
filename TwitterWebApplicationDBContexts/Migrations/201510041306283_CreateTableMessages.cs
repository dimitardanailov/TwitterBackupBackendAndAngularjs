namespace TwitterWebApplicationDBContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableMessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                {
                    MessageID = c.Int(nullable: false, identity: true),
                    UserID = c.String(nullable: false, maxLength: 128),
                    Text = c.String(nullable: false, maxLength: 140),
                })
             .PrimaryKey(t => t.MessageID);

            CreateIndex("dbo.Messages", "UserID");
            AddForeignKey("dbo.Messages", "UserID", "AspNetUsers", "Id");

            AddColumn("dbo.Messages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "datetime"));
            AddColumn("dbo.Messages", "UpdatedAt", c => c.DateTime(nullable: false, storeType: "datetime"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "CreatedAt");
            DropColumn("dbo.Messages", "UpdatedAt");

            DropIndex("dbo.Messages", "UserID");
            DropForeignKey("dbo.Messages", "UserID", "AspNetUsers");
            DropTable("dbo.Messagess");
        }
    }
}
