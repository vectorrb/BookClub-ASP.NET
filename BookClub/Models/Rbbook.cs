using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public partial class Rbbook
    {
        public int BookId { get; set; }
        [Required(ErrorMessage = "Please Enter Book Title")]
        [Display(Name = "Book Title")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Please Enter Book Author")]
        [Display(Name = "Book Author")]
        public string BookAuthor { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
        [Required(ErrorMessage = "Please Enter Image URL")]
        [Display(Name = "Book Image URL")]
        public string BookImageUrl { get; set; }
        [Required(ErrorMessage = "Please select genre")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
    }
}
