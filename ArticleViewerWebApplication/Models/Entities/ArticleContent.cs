using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleViewerWebApplication.Models.Entities
{
    public class ArticleContent
    {
        public List<string> paragraphs { get; set; }

        public List<string> tags { get; set; }
    }
}