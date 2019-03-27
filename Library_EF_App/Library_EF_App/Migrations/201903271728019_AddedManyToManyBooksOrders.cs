namespace Library_EF_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedManyToManyBooksOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookOrders",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.OrderId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookOrders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.BookOrders", "BookId", "dbo.Books");
            DropIndex("dbo.BookOrders", new[] { "OrderId" });
            DropIndex("dbo.BookOrders", new[] { "BookId" });
            DropTable("dbo.BookOrders");
        }
    }
}
