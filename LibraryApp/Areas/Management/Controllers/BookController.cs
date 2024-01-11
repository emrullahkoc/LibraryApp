using EK.Helper.Utils;
using LibraryApp.Models;
using LibraryApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var model = db.Books.Where(c => c.Status == true).OrderBy(x => x.CreatedDate).ToList();
			return View(model);
        }
        public IActionResult Details(int id)
        {
            Book? model = db.Books.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Book/Index");
            }
            return View(model);
        }
        public IActionResult Create()
        {
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", null);
			ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", null);
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
            };
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name"
				, model.CategoryId);
			ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName"
				, model.AuthorId);
			return View(model);
        }

    }
}


