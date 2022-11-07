using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClub.Models
{
    public partial class Rbuser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "PLease Enter Name")]
		[Display(Name = "Name")]
		public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Hobbies")]
        public string Hobbies { get; set; }

        [Display(Name = "Gender")]
        public bool? Gender { get; set; }

        [Required(ErrorMessage = "Please enter Date of Birth")]
		[Display(Name = "Date of Birth")]
		[DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
