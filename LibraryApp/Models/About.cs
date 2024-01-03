using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(64)]
        public string Title { get; set; }
        [StringLength(128)]
        public string? ImageUrl { get; set; }
        [Required]
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
