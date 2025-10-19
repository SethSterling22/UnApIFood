using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnApIFood.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key for Place relationship
        [ForeignKey("PlaceId")]
        public int? PlaceId { get; set; }  = null;

        [Required]
        [StringLength(250)]
        public string? Category { get; set; } = null;
        [Required]
        [StringLength(250)]
        public string? Name { get; set; } = null;

        [Required]
        [StringLength(250)]
        public string? Description { get; set; } = null;

        public double? Price { get; set; } = 0.00;

        public string? ImageURL { get; set; } = "";

        [ForeignKey("UserId")]
        public int? CreatedBy { get; set; } = null;

        public DateTime? Created { get; set; } = null;

        [ForeignKey("UserId")]
        public int? ModifiedBy { get; set; } = null;

        public DateTime? Modified { get; set; } = null;

        // Propiedad de navegación para Lugar
        // public Place Place { get; set; }

    }
}
