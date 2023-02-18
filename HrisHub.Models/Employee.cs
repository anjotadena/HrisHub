using System.ComponentModel.DataAnnotations;

namespace HrisHub.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Employee name is a required field!")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Skillsets { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "Employee email is required field!")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ImagePath { get; set; } = string.Empty;
    }
}
