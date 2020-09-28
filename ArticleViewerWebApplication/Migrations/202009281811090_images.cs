namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        imageID = c.Int(nullable: false, identity: true),
                        imageData = c.Binary(),
                        imageDate = c.DateTime(nullable: false),
                        imageCaption = c.String(),
                        Article_articleId = c.Int(),
                    })
                .PrimaryKey(t => t.imageID)
                .ForeignKey("dbo.Articles", t => t.Article_articleId)
                .Index(t => t.Article_articleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Article_articleId", "dbo.Articles");
            DropIndex("dbo.Images", new[] { "Article_articleId" });
            DropTable("dbo.Images");
        }
    }
}
