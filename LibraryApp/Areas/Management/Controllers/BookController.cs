using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Areas.Management.Controllers
{
    [Area("Management")]

    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
