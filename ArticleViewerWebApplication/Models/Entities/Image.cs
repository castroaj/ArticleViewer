using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticleViewerWebApplication.Models.Entities
{
    public class Image
    {
        [Key]
        public int imageID { get; set; }

        public byte[] imageData { get; set; }

        public DateTime imageDate { get; set; }

        public string imageCaption { get; set; }

    }
}