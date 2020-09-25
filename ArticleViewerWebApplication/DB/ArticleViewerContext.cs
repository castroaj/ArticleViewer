using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;

namespace ArticleViewerWebApplication.DB
{
    public class ArticleViewerContext : DbContext
    {
        public ArticleViewerContext() : base("ArticleViewerContext")
        { }


        public DbSet<User> users { get; set; }
        public DbSet<Article> articles { get; set; }
        public DbSet<Comment> comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Article>().HasMany<Comment>(a => a.comments).WithMany(c => c.articles).Map(cs => {
                cs.MapLeftKey("ArticleRefId");
                cs.MapRightKey("CommentRefId");
                cs.ToTable("ArticleComment");
            });

            modelBuilder.Entity<User>().ToTable("User");
        }

        public static ArticleViewerContext Create()
        {
            return new ArticleViewerContext();
        }




    }
}