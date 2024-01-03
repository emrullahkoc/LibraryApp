using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
