using LibraryApp.Models;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Areas.Management.Controllers
{
	[Area("Management")]
	[Authorize]
	public class HomeController : Controller
	{
		LibraryDbContext db = new LibraryDbContext();

		public LibraryDbContext Db { get => db; set => db = value; }

		public IActionResult Index()
		{
			var model = new DashboardViewModel();
			model.AuthorCount = Db.Authors.Count(x => x.Status == true);
			model.CategoryCount = Db.Categories.Count(x => x.Status == true);
			model.BookCount = Db.Books.Count(x => x.Status == true);
			model.Books = Db.Books
									.Where(x => x.Status == true)
									.OrderByDescending(x => x.CreatedDate)
									.Take(5).ToList();
			return View(model);
		}
	}
}
