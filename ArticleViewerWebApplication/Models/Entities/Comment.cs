using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArticleViewerWebApplication.Models.Entities
{
    public class Comment
    {
        [Key]
        public int commmentId { get; set; }

        public string userId { get; set; }

        public DateTime date { get; set; }

        public int likes { get; set; }

        public string text { get; set; }

        public virtual ICollection<Article> articles { get; set; }


        public Comment()
        {
            this.articles = new HashSet<Article>();
        }
    }
}