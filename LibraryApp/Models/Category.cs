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
        [Required(ErrorMessage = "Kategori alanını doldurun.")]
        [StringLength(100, ErrorMessage = "Kategori alanını en fazla 100 karakter içermeli")]
        [MinLength(5, ErrorMessage = "Kategori alanını alanı en az 3 karakter içermeli.")]
        public string Name { get; set; }
        [StringLength(128)]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Açıklama alanını doldurun.")]
        [StringLength(500, ErrorMessage = "Açıklama alanını en fazla 500 karakter içermeli")]
        [MinLength(15, ErrorMessage = "Açıklama alanını alanı en az 15 karakter içermeli.")]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
