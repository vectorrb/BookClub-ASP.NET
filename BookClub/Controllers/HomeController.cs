using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookClub.Models;
using System.Security.Cryptography;
using System.Text;

namespace BookClub.Controllers
{
	public class HomeController : Controller
	{
		public Db01Context context { get; }

		public HomeController(Db01Context appDBContext)
		{
			context = appDBContext;//public property, will be responsible to do crud operations
		}
		public static string ComputeStringToSha256Hash(string plainText)
		{
			// Create a SHA256 hash from string   
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// Computing Hash - returns here byte array
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

				// now convert byte array to a string   
				StringBuilder stringbuilder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					stringbuilder.Append(bytes[i].ToString("x2"));
				}
				return stringbuilder.ToString();
			}
		}

			public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Join()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Join(Rbuser user)
		{
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");
			Debug.WriteLine(user.FirstName);
			Debug.WriteLine(user.Hobbies);
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");

			user.Password = ComputeStringToSha256Hash(user.Password);
			context.Rbusers.Add(user);
			context.SaveChanges();
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginView user)
		{
			Debug.WriteLine("====================================");
			Debug.WriteLine(user.userName);
			Debug.WriteLine("====================================");
			Rbuser fetchUserData = context.Rbusers
				.Where(m => m.UserName == user.userName && m.ConfirmPassword == user.password)
				.FirstOrDefault();

			if (fetchUserData == null)
			{
				return View();
			}
			else
			{
				TempData["username"] = fetchUserData.FirstName;
				TempData["userId"] = fetchUserData.UserId;
			return RedirectToAction("GetBooksList", "Book"); ;
			}

		}

		[HttpGet]
		public IActionResult AllUsers()
		{
			List<Rbuser> fetchUsers = context.Rbusers.ToList();
			return View(fetchUsers);
		}

		public IActionResult Logout()
		{
			return RedirectToAction(nameof(Index));
		}
	}
}
