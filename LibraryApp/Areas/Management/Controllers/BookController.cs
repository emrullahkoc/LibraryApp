using EK.Helper.Utils;
using LibraryApp.Models;
using LibraryApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Areas.Management.Controllers
{
	[Area("Management")]

	public class BookController : Controller
	{
		LibraryDbContext db = new LibraryDbContext();

		private readonly IWebHostEnvironment _hostEnvironment; //resim eklemek için
		public BookController(IWebHostEnvironment hostEnviroment) //ctor tabla
		{
			_hostEnvironment = hostEnviroment;
		}
		
		public IActionResult Index()
		{
			var model = db.Books.Where(c => c.Status == true).OrderBy(x => x.CreatedDate).Include("Author").Include("Category").ToList();
			return View(model);
		}
		public IActionResult Details(int id)
		{
			var model = db.Books.Include("Author").Include("Category")
				.FirstOrDefault(x => x.Id == id);
			if (model == null)
			{
				return Redirect("/Management/Book/Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.Status == true), "Id", "Name", null);
			ViewBag.AuthorId = new SelectList(db.Authors.Where(c => c.Status == true), "Id", "FullName", null);
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Book model, IFormFile? img)
		{
			if (ModelState.IsValid)
			{
				if (img != null)
				{
					model.ImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
				}
				model.CreatedDate = DateTime.Now;
				model.PublishDate = DateTime.Now;
				model.Status = true;

				db.Books.Add(model);
				db.SaveChanges();
				return Redirect("/Management/Book/Index");
			}
			ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.Status == true), "Id", "Name"
				, model.CategoryId);
			ViewBag.AuthorId = new SelectList(db.Authors.Where(c => c.Status == true), "Id", "FullName"
				, model.AuthorId);
			return View(model);
		}
		public IActionResult Edit(int id)
		{
			var model = db.Books.Find(id);
			if (model == null)
			{
				return Redirect("/Management/Book/Index");
			}
			ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.Status == true), "Id", "Name"
				, model.CategoryId);
			ViewBag.AuthorId = new SelectList(db.Authors.Where(c => c.Status == true), "Id", "FullName"
				, model.AuthorId);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Book model, IFormFile? img)
		{
			if (ModelState.IsValid)
			{
				var editModel = db.Books.Find(model.Id);
				if (editModel == null)
				{
					return Redirect("/Management/Book/Index");
				}

				if (img != null)
				{
					await ImageUploader.DeleteImageAsync(_hostEnvironment, editModel.ImageUrl);
					editModel.ImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
				}
				editModel.PublishDate = DateTime.Now;
				editModel.Name = model.Name;
				editModel.PageCount = model.PageCount;
				editModel.Barcode = model.Barcode;
				editModel.Brief = model.Brief;
				editModel.CategoryId = model.CategoryId;
				editModel.AuthorId = model.AuthorId;
				db.SaveChanges();
				return Redirect("/Management/Book/Index");
			}
			ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.Status == true), "Id", "Name", model.CategoryId);
			ViewBag.AuthorId = new SelectList(db.Authors.Where(c => c.Status == true), "Id", "FullName", model.AuthorId);
			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var book = await db.Books.FindAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			db.Books.Remove(book);
			await db.SaveChangesAsync();

			return RedirectToAction("/Management/Book/Index");
		}

		[HttpPost]
		public async Task<JsonResult> DeleteByJs(int id)
		{
			var book = await db.Books.FindAsync(id);

			if (book == null)
			{
				return Json("Böyle Bir Kitap Bulunamadı");
			}

			db.Books.Remove(book);
			await db.SaveChangesAsync();

			return Json("Silindi");
		}
	}
}


