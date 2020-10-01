namespace ArticleViewerWebApplication.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ArticleViewerWebApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ArticleViewerWebApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArticleViewerWebApplication.Models.ApplicationDbContext context)
        {

        }
    }
}
