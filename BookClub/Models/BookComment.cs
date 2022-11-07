using System.Collections.Generic;

namespace BookClub.Models
{
	public class BookComment
	{
		public Rbbook book { get; set; }
		public List<CommentUserName> comment { get; set; }
	}
}
