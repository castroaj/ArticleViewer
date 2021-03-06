﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

        // GET: MyArticles/Create
        public ActionResult Create()
        {
            ViewBag.numOfParagraphs = 3;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfigureParagraphs(string page, int numOfParagraphs, int? articleId)
        {
            ViewBag.numOfParagraphs = numOfParagraphs;

            if (page != null && page.Equals("Edit"))
            {
                Article article = db.articles.Find(articleId);

                GetArticle(articleId);
                ViewBag.numOfParagraphs = numOfParagraphs;

                return View("Edit", article);
            }
            return View("Create");

        }

        // GET: MyArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = GetArticle(id);

            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "articleId,articlePreview,title")] Article article, List<string> paragraphs)
        {
            if (ModelState.IsValid)
            {
                ArticleContent ac = new ArticleContent
                {
                    paragraphs = paragraphs
                };

                Article foundArticle = db.articles.Find(article.articleId);

                foundArticle.title = article.title;
                foundArticle.articlePreview = article.articlePreview;
                foundArticle.articleContent = JsonConvert.SerializeObject(ac);



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

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        public Article GetArticle(int? id)
        {
            Article article = db.articles.Find(id);

            if (article.articleContent != null)
            {
                article.content = JsonConvert.DeserializeObject<ArticleContent>(article.articleContent);
                article.content.paragraphs.ForEach(p => p = "\t" + p);
                ViewBag.numOfParagraphs = article.content.paragraphs.Count;
            }
            return article;
        }


    }
}
