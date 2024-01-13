using LibraryApp.Models;
using LibraryApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Areas.Management.Controllers
{
    [Area("Management")]

    public class CategoryController : Controller
    {
        LibraryDbContext db = new LibraryDbContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryController(IWebHostEnvironment hostEnviroment) //ctor tabla
        {
            _hostEnvironment = hostEnviroment;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> model = db.Categories
                .Where(c => c.Status == true)
                .ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Category? model = db.Categories.Include(a => a.Books).ThenInclude(b => b.Author).FirstOrDefault(a => a.Id == id);
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.ImageURL = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                model.Status = true;
                model.CreatedDate = DateTime.Now;
                db.Categories.Add(model);
                db.SaveChanges();
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category? model = db.Categories.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category model, IFormFile? img)
        {
            if (ModelState.IsValid)
            {
                Category? editmodel = db.Categories.Find(model.Id);
                if (editmodel == null)
                {
                    return Redirect("/Management/Category/Index");
                }
                if (img != null)
                {
                    await ImageUploader.DeleteImageAsync(_hostEnvironment, editmodel.ImageURL);
                    editmodel.ImageURL = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                editmodel.Name = model.Name;
                editmodel.Description = model.Description;
                editmodel.Status = true;
                editmodel.CreatedDate = DateTime.Now;
                await db.SaveChangesAsync();
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category? model = db.Categories.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Category? model = db.Categories.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Category/Index");
            }
            model.Status = false;
            db.SaveChanges();
            return Redirect("/Management/Category/Index");
        }
    }
}
