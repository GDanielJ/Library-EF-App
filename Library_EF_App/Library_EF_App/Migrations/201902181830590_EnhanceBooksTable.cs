namespace Library_EF_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnhanceBooksTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Name", c => c.String());
        }
    }
}
