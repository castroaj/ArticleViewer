using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ArticleViewerWebApplication.DB;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
