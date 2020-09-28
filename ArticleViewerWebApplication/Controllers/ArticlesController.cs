using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ArticleViewerWebApplication.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            return View(db.articles.OrderByDescending(a => a.date).ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string articleId, string text)
        {
            Comment newComment = new Comment()
            {
                text = text,
                date = DateTime.Now,
                likes = 0,
                userName = User.Identity.GetUserName()
            };

            int articleIdConvert = int.Parse(articleId);


            Article article = db.articles.Where(a => a.articleId == articleIdConvert).FirstOrDefault();

            ViewBag.article = articleIdConvert;

            article.comments.Add(newComment);
            db.SaveChanges();
            return View("ViewArticle", article);
        }


        public ActionResult ViewArticle(int? articleId)
        {
            if (articleId != null)
            {
                Article selectedArticle = db.articles.Find(articleId);

                if (selectedArticle == null)
                {
                    return View("ErrorPage");
                }

                if (selectedArticle.content != null)
                {
                    selectedArticle.content = JsonConvert.DeserializeObject<ArticleContent>(selectedArticle.articleContent);
                    selectedArticle.content.paragraphs.ForEach(p => p = "\t" + p);
                }

                return View(selectedArticle);
            }
            return View("ErrorPage");
        }

    }
}
