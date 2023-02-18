using System.ComponentModel.DataAnnotations;

namespace HrisHub.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Email is a required field!")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "Password is a required field!")]
        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
