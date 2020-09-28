using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;
using Microsoft.AspNet.Identity;

namespace ArticleViewerWebApplication.Controllers
{
    public class ArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articles
        public ActionResult Index()
        {

            return View(db.articles.OrderByDescending(a => a.date).ToList());
        }

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
            return View("Index", db.articles.OrderByDescending(a => a.date).ToList());
        }


        public ActionResult ViewArticle(int? articleId)
        {
            if (articleId != null)
            {
                Article selectedArticle = db.articles.Where(a => a.articleId == articleId).FirstOrDefault();

                if (selectedArticle == null)
                {
                    return View("ErrorPage");
                }

                return View(selectedArticle);
            }
            return View("ErrorPage");
        }

    }
}
