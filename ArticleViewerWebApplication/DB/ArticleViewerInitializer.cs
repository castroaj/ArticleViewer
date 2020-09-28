using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;

namespace ArticleViewerWebApplication.DB
{
    public class ArticleViewerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var articles = new List<Article>
            {
                new Article{articleId=1, author="Test", date=DateTime.Now, body="Body", title="Header"}
            };
            articles.ForEach(a => context.articles.Add(a));

            context.SaveChanges();
        }
    }
}