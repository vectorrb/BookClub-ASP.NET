using System;
using System.Collections.Generic;

namespace BookClub.Models
{
    public partial class Rbcomment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
    }
}
