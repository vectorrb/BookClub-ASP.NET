using System;
using System.Collections.Generic;

namespace BookClub.Models
{
    public partial class RbusersLikesDislike
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool LikeDislike { get; set; }
    }
}
