using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Author? Author { get; set; }

        [Required, StringLength(32)]
        public string Name { get; set; }
        [Required, StringLength(256)]
        public string Brief { get; set; }
        [Required]
        public int PageCount { get; set; }
        [Required, StringLength(32)]
        public string Barcode { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
