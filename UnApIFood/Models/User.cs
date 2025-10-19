using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnApIFood.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Role { get; set; } = "user";

        [Required]
        [StringLength(250)]
        public string? Email { get; set; } = null;

        [Required]
        [StringLength(250)]
        public string? Password { get; set; } = null;

        [Required]
        [StringLength(250)]
        public string? FirstName { get; set; } = null;

        [Required]
        [StringLength(250)]
        public string? LastName { get; set; } = null;
        public DateTime? LastLogin { get; set; } = null;
        public DateTime? RegisteredOn { get; set; } = null;


    }
}
