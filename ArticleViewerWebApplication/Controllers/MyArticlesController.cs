using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArticleViewerWebApplication.DB;
using ArticleViewerWebApplication.Models;
using ArticleViewerWebApplication.Models.Entities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ArticleViewerWebApplication.Controllers
{
    [Authorize]
    public class MyArticlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyArticles
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var articles = db.articles.Where(a => a.userId.Equals(userId)).ToList();

            return View(articles);
        }

        // GET: MyArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: MyArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "articlePreview,title,body")] Article article, HttpPostedFileBase imageData, List<string> paragraphs)
        {
            if (ModelState.IsValid)
            {
                // Configure Article metadata
                article.userId = User.Identity.GetUserId();
                article.date = DateTime.Now;
                article.author = User.Identity.GetUserName();

                if (imageData != null)
                { 

                    byte[] imageByteArray = ConvertToByte(imageData);

                    article.image = 
                    new Image {
                        imageDate = DateTime.Now,
                        imageCaption = "Caption",
                        imageData = imageByteArray
                    };
                }

                ArticleContent ac = new ArticleContent
                {
                    paragraphs = paragraphs
                };

                article.articleContent = JsonConvert.SerializeObject(ac);

                db.articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: MyArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "articleId,userId,author,date,articlePreview,title,body")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: MyArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: MyArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.articles.Find(id);
            db.articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

    }
}
