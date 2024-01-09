using LibraryApp.Models;
using LibraryApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LibraryApp.Areas.Management.Controllers
{
    [Area("Management")]
    public class AuthorController : Controller
    {
        LibraryDbContext db = new LibraryDbContext();
        private readonly IWebHostEnvironment _hostEnvironment;
        public AuthorController(IWebHostEnvironment hostEnviroment) //ctor tabla
        {
            _hostEnvironment = hostEnviroment;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> model = db.Authors
                .Where(c => c.Status == true)
                .ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Author? model = db.Authors.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Author/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    model.ImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
                }
                if (model.DeathDate == null)
                {
                    model.DeathDate = null;
                }
                else
                {
                    model.DeathDate = model.DeathDate;
                }
                model.Status = true;
                model.CreatedDate = DateTime.Now;
				db.SaveChanges();
                return Redirect("Management/Author/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Author? model = (Author)db.Authors.Find(id);
            if (model == null)
            {
                return Redirect("Management/Author/Index");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Author model, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                Author? editmodel = db.Authors.Find(model.Id);
                if (editmodel == null)
                {
                    return Redirect("/Management/Author/Index");
                }
                if (img != null)
                {
                    await ImageUploader.DeleteImageAsync(_hostEnvironment, editmodel.ImageUrl);
                    editmodel.ImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);

                }
                editmodel.FullName = model.FullName;
                editmodel.Description = model.Description;
                editmodel.CreatedDate = DateTime.Now;
                editmodel.BirthDate = model.BirthDate;
                if (model.DeathDate == null)
                {
					editmodel.DeathDate = null;
				}
                else
                {
					editmodel.DeathDate = model.DeathDate;
				}
				db.SaveChanges();
                return Redirect("/Management/Author/Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Author? model = db.Authors.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Author/Index");
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Author? model = db.Authors.Find(id);
            if (model == null)
            {
                return Redirect("/Management/Author/Index");
            }
            model.Status = false;
            db.SaveChanges();
            return Redirect("/Management/Author/Index");
        }

    }
}
