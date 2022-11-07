using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
	public class LoginView
	{
		[Required(ErrorMessage = "Username required")]
		public string userName { get; set; }
		[Required(ErrorMessage = "password required")]
		public string password { get; set; }
	}
}
