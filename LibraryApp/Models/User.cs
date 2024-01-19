using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
	public class User {
		[Key]
		public int Id { get; set; }
		[Required, StringLength(100)]
		public string FullName { get; set; }
		[Required]
		public DateTime BirthDate { get; set; }
		[Required, StringLength(100)]
		public string Email { get; set; }
		[Required, StringLength(13)]
		public string? Phone { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }

    }
}
