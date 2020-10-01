namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        articleId = c.Int(nullable: false, identity: true),
                        userId = c.String(maxLength: 128),
                        author = c.String(),
                        date = c.DateTime(nullable: false),
                        title = c.String(),
                        articlePreview = c.String(),
                        articleContent = c.String(),
                        image_imageID = c.Int(),
                    })
                .PrimaryKey(t => t.articleId)
                .ForeignKey("dbo.Images", t => t.image_imageID)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId)
                .Index(t => t.image_imageID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        commmentId = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        date = c.DateTime(nullable: false),
                        likes = c.Int(nullable: false),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.commmentId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        imageID = c.Int(nullable: false, identity: true),
                        imageData = c.Binary(),
                        imageDate = c.DateTime(nullable: false),
                        imageCaption = c.String(),
                    })
                .PrimaryKey(t => t.imageID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CommentArticles",
                c => new
                    {
                        Comment_commmentId = c.Int(nullable: false),
                        Article_articleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_commmentId, t.Article_articleId })
                .ForeignKey("dbo.Comments", t => t.Comment_commmentId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_articleId, cascadeDelete: true)
                .Index(t => t.Comment_commmentId)
                .Index(t => t.Article_articleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Articles", "image_imageID", "dbo.Images");
            DropForeignKey("dbo.CommentArticles", "Article_articleId", "dbo.Articles");
            DropForeignKey("dbo.CommentArticles", "Comment_commmentId", "dbo.Comments");
            DropIndex("dbo.CommentArticles", new[] { "Article_articleId" });
            DropIndex("dbo.CommentArticles", new[] { "Comment_commmentId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Articles", new[] { "image_imageID" });
            DropIndex("dbo.Articles", new[] { "userId" });
            DropTable("dbo.CommentArticles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Images");
            DropTable("dbo.Comments");
            DropTable("dbo.Articles");
        }
    }
}
