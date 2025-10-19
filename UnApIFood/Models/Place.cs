using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnApIFood.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key for Place relationship
        [ForeignKey("UniversityId")]
        public int? UniversityId { get; set; }  = null;

        [Required]
        [StringLength(25)]
        public string? Name { get; set; } = "";

        [StringLength(250)]
        public string? Address { get; set; } = null;

        // Propiedades de relación
        public string? Schedule { get; set; } = null;
        public string? PriceAverage { get; set; } = "";

        public string? Description { get; set; } = "";
        
        public string? ImageURL { get; set; } = "";

        [ForeignKey("UserId")]
        public int? CreatedBy { get; set; } = null;

        public DateTime? Created { get; set; } = null;

        [ForeignKey("UserId")]
        public int? ModifiedBy { get; set; } = null;

        public DateTime? Modified { get; set; } = null;

    }
}
