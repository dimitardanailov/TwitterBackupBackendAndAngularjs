namespace TwitterWebApplicationDBContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatetimeFieldsTableMessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "CreatedAt", c => c.DateTime(nullable: false, storeType: "datetime"));
            AddColumn("dbo.Messages", "UpdatedAt", c => c.DateTime(nullable: false, storeType: "datetime"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "CreatedAt");
            DropColumn("dbo.Messages", "UpdatedAt");
        }
    }
}
