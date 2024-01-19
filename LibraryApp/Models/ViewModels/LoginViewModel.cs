using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models.ViewModels {
	public class LoginViewModel {
        [Required(ErrorMessage = "Lütfen email giriniz")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Lütfen şifre giriniz")]
		public string Password { get; set; }
    }
}
