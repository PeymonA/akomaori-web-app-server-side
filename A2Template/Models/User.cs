using System.ComponentModel.DataAnnotations;

namespace A2.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}