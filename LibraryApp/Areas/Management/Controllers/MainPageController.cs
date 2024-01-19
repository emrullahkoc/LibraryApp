using EK.Helper.Utils;
using LibraryApp.Models;
using LibraryApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApp.Areas.Management.Controllers
{
	[Area("Management")]
	[Authorize]

	public class MainPageController : Controller
	{
		LibraryDbContext db = new LibraryDbContext(); 
		private readonly IWebHostEnvironment _hostEnvironment; //Ne İşe Yarıyor ?

		public MainPageController(IWebHostEnvironment hostEnvironment) //Ne İşe Yarıyor ?
		{
			_hostEnvironment = hostEnvironment;
		}

		public IActionResult Index()
		{
			MainPage model = db.MainPages.FirstOrDefault();
			return View(model);
		}
		public IActionResult Edit(int id)
		{
			MainPage? model = db.MainPages.Find(id);
			if (model == null)
			{
				return Redirect("/Management/MainPage/Index");
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(MainPage model, IFormFile? img)
		{
			if (ModelState.IsValid)
			{
                var editModel = db.MainPages.Find(model.Id);
                if (editModel == null)
				{
					return Redirect("/Management/MainPage/Index");
				}
				if (img != null)
				{
					await ImageUploader.DeleteImageAsync(_hostEnvironment, editModel.ImageUrl);
					editModel.ImageUrl = await ImageUploader.UploadImageAsync(_hostEnvironment, img);
				}
                model.CreatedDate = DateTime.Now;
				editModel.Title = model.Title.ToUpper();
				editModel.Description = model.Description;
				db.SaveChanges();

				//mail göndermek için
				var mailList = new List<String>() {
				"emrullahkoc.1995@gmail.com"
				};

				MailSender.Send(mailList, "Ana Sayfa Düzenlendi", $"Ana Sayfada değişiklik yapıldı </br></br> {editModel.Title} </br> {editModel.Description} </br> Düzenlenme Tarihi {model.CreatedDate}");

				return Redirect("/Management/MainPage/Index");
			}
			return View(model);

		}

	}
}
