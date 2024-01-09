using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Soyisim alanını doldurun.")]
        [StringLength(100, ErrorMessage = "İsim Soyisim alanını en fazla 100 karakter içermeli")]
        [MinLength(5, ErrorMessage = "İsim Soyisim alanını alanı en az 5 karakter içermeli.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Hakkında alanını doldurun.")]
        [StringLength(500, ErrorMessage = "Hakkında alanını en fazla 500 karakter içermeli")]
        [MinLength(15, ErrorMessage = "Hakkında alanını alanı en az 15 karakter içermeli.")]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Doğum günü alanını doldurun.")]
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Book> Books { get; set; } 
    }
}
