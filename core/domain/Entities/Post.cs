using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace core.domain.Entities
{
    public class Post
    {
        [Key]
        public int postID { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }

    }
}
