using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class MainPage
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Başlık alanını doldurun.")]
        [StringLength(64, ErrorMessage = "Başlık en fazla 32 karakter içermeli")]
        [MinLength(5, ErrorMessage = "Başlık alanı en az 5 karakter içermeli.")]
        public string Title { get; set; }

        [StringLength(128, ErrorMessage = "Başlık en fazla 128 karakter içermeli")]
        public string? ImageUrl { get; set; }


        [Required(ErrorMessage = "Açıklama alanını doldurun.")]
        [StringLength(250, ErrorMessage = "Açıklama en fazla 250 karakter içermeli")]
        [MinLength(10, ErrorMessage = "Açıklama alanı en az 10 karakter içermeli.")]
        public string Description { get; set; }


        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
