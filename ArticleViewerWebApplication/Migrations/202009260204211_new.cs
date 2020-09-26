namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "img_caption");
            DropColumn("dbo.Articles", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "test", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "img_caption", c => c.String());
        }
    }
}
