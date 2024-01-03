using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string FirstName { get; set; }
        [Required, StringLength(32)]
        public string LastName { get; set; }
        [Required, StringLength(32)]
        public string Title { get; set; }
        [Required, StringLength(256)]
        public string Details { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Read { get; set; }
        public string? Reply { get; set; }
    }
}
