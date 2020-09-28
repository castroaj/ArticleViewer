namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "userName", c => c.String());
            DropColumn("dbo.Comments", "userId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "userId", c => c.String());
            DropColumn("dbo.Comments", "userName");
        }
    }
}
