namespace Library_EF_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Firstname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Users", "Lastname", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lastname", c => c.String());
            AlterColumn("dbo.Users", "Firstname", c => c.String());
        }
    }
}
