using LibraryApp.Models.ViewModels;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApp.Controllers
{
	public class UserLoginController : Controller
	{
		LibraryDbContext db = new LibraryDbContext();
		public IActionResult Login()
		{
			ViewBag.Message = "";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = db.PanelUsers.FirstOrDefault(
					x => x.Email == model.Email
					&& x.Password == model.Password
					&& x.Status == true);

				#region Kullanıcı Yoksa
				if (user == null)
				{
					ViewBag.Message = "Lütfen bilgilerinizi kontrol edin";
					return View(model);
				}

				#endregion

				// Cookie kısmında verileri şifreli olarak tutması için hangi veriler 
				//olduğunu belirtiyoruz.
				var claims = new List<Claim>
				{
						new Claim(ClaimTypes.NameIdentifier, user.UserName),
						new Claim(ClaimTypes.Name, user.UserName),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim("FullName", user.FullName),
						new Claim(ClaimTypes.Role, user.Role),
						new Claim("ImagePath", user.ImageUrl ?? "")
				};

				// Claimleri identity olarak işlemek için
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					AllowRefresh = true,
					// Refreshing the authentication session should be allowed.
					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
								new ClaimsPrincipal(claimsIdentity),
								authProperties);
				return RedirectToAction("Index", "Management");
			}
			ViewBag.Message = "";
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
