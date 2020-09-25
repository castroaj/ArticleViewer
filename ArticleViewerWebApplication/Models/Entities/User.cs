using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticleViewerWebApplication.Models.Entities
{
    public class User
    {
        [Key]
        public int userTableId { get; set; }

        public string userId { get; set; }
    }
}