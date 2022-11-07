using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Models
{
	public class BookGenreView
	{
		[Required(ErrorMessage = "Please Enter Book Information")]
		public Rbbook book { get; set; }
		public Genre bookGenre { get; set; }
	}
}
