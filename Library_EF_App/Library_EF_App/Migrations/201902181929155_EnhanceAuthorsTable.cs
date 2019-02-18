namespace Library_EF_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnhanceAuthorsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "Author_Id" });
            RenameColumn(table: "dbo.Books", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Authors", "Firstname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Authors", "Lastname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            AlterColumn("dbo.Books", "AuthorId", c => c.Int());
            AlterColumn("dbo.Authors", "Lastname", c => c.String());
            AlterColumn("dbo.Authors", "Firstname", c => c.String());
            RenameColumn(table: "dbo.Books", name: "AuthorId", newName: "Author_Id");
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
