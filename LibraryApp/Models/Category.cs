using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }
        [StringLength(128)]
        public string? ImageURL { get; set; }
        [Required, StringLength(256)]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
