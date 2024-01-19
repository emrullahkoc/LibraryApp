using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
	public class PanelUser
	{
		[Key] 
		public Guid Id { get; set; }
		[Required, StringLength(100)]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required, StringLength(64)]

		public string FullName { get; set; }
		[StringLength(128)]
		public string? ImageUrl { get; set; }
		[Required, StringLength(32)]
		public string Role { get; set; }
		[Required, StringLength(32)]
		public string UserName { get; set; }

		public bool Status { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
