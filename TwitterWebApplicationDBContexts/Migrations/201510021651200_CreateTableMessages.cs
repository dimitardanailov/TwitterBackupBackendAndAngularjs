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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", "UserID");
            DropForeignKey("dbo.Messages", "UserID", "AspNetUsers");
            DropTable("dbo.Messagess");
        }
    }
}
