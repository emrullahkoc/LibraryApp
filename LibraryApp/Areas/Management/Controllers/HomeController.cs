using LibraryApp.Models;
using LibraryApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Areas.Management.Controllers
{
	[Area("Management")]

	public class HomeController : Controller
	{
		LibraryDbContext db = new LibraryDbContext();

		public IActionResult Index()
		{
			var model = new DashboardViewModel();
			model.AuthorCount = db.Authors.Count(x => x.Status == true);
			model.CategoryCount = db.Categories.Count(x => x.Status == true);
			model.BookCount = db.Books.Count(x => x.Status == true);
			model.Books = db.Books
									.Where(x => x.Status == true)
									.OrderByDescending(x => x.CreatedDate)
									.Take(5).ToList();
			return View(model);
		}
	}
}
