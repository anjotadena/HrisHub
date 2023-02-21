using System.ComponentModel.DataAnnotations;

namespace HrisHub.Models
{
    public class Role
    {
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}
