using ArticleViewerWebApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArticleViewerWebApplication.Models
{
    public class Article
    {

        [Key]
        public int articleId { get; set; }

        public string userId { get; set; }

        public string author { get; set; }

        public DateTime date { get; set; }

        public string title { get; set; }

        public string articlePreview { get; set; }

        [NotMapped]
        public ArticleContent content { get; set; }

        public string articleContent { get; set; }

        public virtual ICollection<Comment> comments { get; set; }

        public virtual Image image { get; set; }

        public Article()
        {
            this.comments = new HashSet<Comment>();
        }
    }
}