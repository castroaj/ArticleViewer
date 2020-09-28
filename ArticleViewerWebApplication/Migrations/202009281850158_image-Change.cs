namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Article_articleId", "dbo.Articles");
            DropIndex("dbo.Images", new[] { "Article_articleId" });
            AddColumn("dbo.Articles", "image_imageID", c => c.Int());
            CreateIndex("dbo.Articles", "image_imageID");
            AddForeignKey("dbo.Articles", "image_imageID", "dbo.Images", "imageID");
            DropColumn("dbo.Images", "Article_articleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Article_articleId", c => c.Int());
            DropForeignKey("dbo.Articles", "image_imageID", "dbo.Images");
            DropIndex("dbo.Articles", new[] { "image_imageID" });
            DropColumn("dbo.Articles", "image_imageID");
            CreateIndex("dbo.Images", "Article_articleId");
            AddForeignKey("dbo.Images", "Article_articleId", "dbo.Articles", "articleId");
        }
    }
}
