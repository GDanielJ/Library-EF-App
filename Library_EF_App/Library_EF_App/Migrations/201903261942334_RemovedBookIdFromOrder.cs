namespace Library_EF_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBookIdFromOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BookId", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "BookId" });
            DropColumn("dbo.Orders", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "BookId");
            AddForeignKey("dbo.Orders", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
