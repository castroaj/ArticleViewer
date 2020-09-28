namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Again : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "articleContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "articleContent", c => c.Binary());
        }
    }
}
