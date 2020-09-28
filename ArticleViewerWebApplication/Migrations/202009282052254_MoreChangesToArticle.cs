namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreChangesToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "articleContent", c => c.Binary());
            DropColumn("dbo.Articles", "body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "body", c => c.String());
            DropColumn("dbo.Articles", "articleContent");
        }
    }
}
