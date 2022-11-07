using BookClub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BookClub.Controllers
{
	public class BookController : Controller
	{
		public Db01Context context { get; }
		public BookController(Db01Context dbContext)
		{
			context = dbContext;
		}

		public enum GenreD
		{
			Drama,
			Fantasy,
			Historical,
			Horror,
			Mystery,
			Mythology,
			BiographyAutobiography,
			NonFiction,
			SelfHelp,
			Suspense
		}

		public IActionResult GetBooksList()
		{
			List<Rbbook> bookList = context.Rbbooks.ToList();
			return View(bookList);
		}
		public IActionResult AllBooks()
		{
			List<Rbbook> bookList = context.Rbbooks.ToList();

			foreach (Rbbook book in bookList)
			{
				if (book.Genre == "6")
					book.Genre = "Biography/AutoBiography";
				else if (book.Genre == "7")
					book.Genre = "Non-Fiction";
				else if (book.Genre == "8")
					book.Genre = "Self-Help";
				else
					book.Genre = ((GenreD)(Int32.Parse(book.Genre))).ToString();
			}
			return View(bookList);
		}
		//public IActionResult Read(int id) {
		//	User user = context.Users.FirstOrDefault(u=> u.userId = Int32.Parse(id));
		//	TempData["username"] = user.userName;
		//	return RedirectToAction("GetBooksList"); 
		//}

		public ActionResult Detail(int id)
		{
			Rbbook book = context.Rbbooks.FirstOrDefault(x => x.BookId == id);
			//return View(book);
			if (book.Genre == "6")
				book.Genre = "Biography/AutoBiography";
			else if (book.Genre == "7")
				book.Genre = "Non-Fiction";
			else if (book.Genre == "8")
				book.Genre = "Self-Help";
			else
				book.Genre = ((GenreD)(Int32.Parse(book.Genre))).ToString();


			BookComment bookComment = new BookComment();
			bookComment.book = context.Rbbooks.FirstOrDefault(x => x.BookId == id);

			List<Rbcomment> comments = context.Rbcomments
				.Where(c => c.BookId == id)
				.ToList();

			List<CommentUserName> commentUserNames = new List<CommentUserName>();
			foreach (Rbcomment comment in comments)
			{
				Rbuser user = context.Rbusers.FirstOrDefault(u => u.UserId == comment.UserId);
				CommentUserName commentUserName = new CommentUserName()
				{
					comment = comment,
					userName = user.FirstName
				};
				commentUserNames.Add(commentUserName);
			}



			bookComment.comment = commentUserNames;
			return View(bookComment);
		}

		public ActionResult Genre(string genre)
		{
			List<Rbbook> booksByGenre = context.Rbbooks
				.Where(b => b.Genre == genre)
				.ToList();
			return View(booksByGenre);
		}

		[HttpGet]
		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Add(BookGenreView newBookView, Rbbook neBook)
		{
			Rbbook newBook = newBookView.book;
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");
			Debug.WriteLine(newBook.Genre);
			Debug.WriteLine(newBook.BookAuthor);
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");

			string genre = neBook.Genre;
			GenreD g = (GenreD)Enum.Parse(typeof(GenreD), genre);
			int genreValue = (int)g;
			Debug.WriteLine((int)g);

			genre = genreValue.ToString();
			newBook.Genre = genre;
			newBook.Likes = 0;
			newBook.Dislikes = 0;

			context.Rbbooks.Add(newBook);
			context.SaveChanges();
			return RedirectToAction(nameof(AllBooks));
		}

		[HttpGet]
		public IActionResult Likedbooks(int userId)
		{
			List<RbusersLikesDislike> likedBooksList = context.RbusersLikesDislikes
				.Where(c => c.UserId == userId && c.LikeDislike == true)
				.ToList();
			List<Rbbook> likedBooks = new List<Rbbook>();

			foreach (RbusersLikesDislike item in likedBooksList)
			{
				Rbbook rbbook = context.Rbbooks.FirstOrDefault(b => b.BookId == item.BookId);
				int value = Int32.Parse(rbbook.Genre);
				var enumm = (GenreD)value;
				rbbook.Genre = ((GenreD)(Int32.Parse(rbbook.Genre))).ToString();
				likedBooks.Add(rbbook);
			}


			return View(likedBooks);
		}

		public ActionResult Edit(int? id)
		{
			if (id == null || id < 0)
				return BadRequest();
			Rbbook rbbook = context.Rbbooks.FirstOrDefault(b => b.BookId == id);
			BookGenreView bookGenreView = new BookGenreView();
			if (rbbook == null)
				return NotFound();
			else
			{
				rbbook.Genre = ((GenreD)(Int32.Parse(rbbook.Genre))).ToString();
				Debug.WriteLine(rbbook.Genre);
				bookGenreView.book = rbbook;
				return View(bookGenreView);
			}

		}
		[HttpPost]
		public ActionResult Edit(int id, BookGenreView modifiedBookView, Rbbook rbbook)
		{
			//if (!ModelState.IsValid)
			//	return BadRequest();
			//else
			//{
			Rbbook modifiedBook = modifiedBookView.book;
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");
			Debug.WriteLine(modifiedBook.BookAuthor);
			Debug.WriteLine(modifiedBook.Genre);
			Debug.WriteLine("====================================");
			Debug.WriteLine("====================================");
			string genre = rbbook.Genre;
			GenreD g = (GenreD)Enum.Parse(typeof(GenreD), genre);
			int genreValue = (int)g;
			Debug.WriteLine((int)g);

			genre = genreValue.ToString();
			modifiedBook.Genre = genre;
			Rbbook fetchBook = context.Rbbooks.FirstOrDefault(b => b.BookId == id);
			fetchBook.Genre = genre;
			fetchBook.BookName = modifiedBook.BookName;
			fetchBook.BookAuthor = modifiedBook.BookAuthor;
			fetchBook.BookImageUrl = modifiedBook.BookImageUrl;
			fetchBook.Description = modifiedBook.Description;
			context.Rbbooks.Update(fetchBook);
			context.SaveChanges();
			return RedirectToAction(nameof(AllBooks));
			//}
		}

		public IActionResult Delete(int id)
		{
			Rbbook rbbook = context.Rbbooks.FirstOrDefault(b => b.BookId == id);
			Debug.WriteLine(id);
			context.Rbbooks.Remove(rbbook);
			context.SaveChanges();
			return RedirectToAction(nameof(AllBooks));
		}

		//public IActionResult AddComment(int id, int bookid)
		//{

		//	return View();
		//}

	}
}
