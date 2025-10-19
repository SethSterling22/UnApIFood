using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnApIFood.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string? Address { get; set; } = "";

        [Required]
        [StringLength(25)]
        public string? Name { get; set; } = "";

        [Required]
        [StringLength(250)]
        public string? Description { get; set; } = null;

        public string? ImageURL { get; set; } = null;

        [ForeignKey("UserId")]
        public int? CreatedBy { get; set; } = null;

        public DateTime? Created { get; set; } = null;
        
        [ForeignKey("UserId")]
        public int? ModifiedBy { get; set; } = null;


        public DateTime? Modified { get; set; } = null;

        // Propiedad de navegación para Lugares
        // public ICollection<Place> Places { get; set; }


        // // Foreign Key for Local relationship
        // [ForeignKey("LocalId")]
        // public int? LocalId { get; set; }  = null;
    }
}