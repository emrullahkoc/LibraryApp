using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Areas.Management.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
