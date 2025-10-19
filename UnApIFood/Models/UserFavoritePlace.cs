using System.ComponentModel.DataAnnotations;

namespace UnApIFood.Models
{
    public class UserFavoritePlace
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PlaceId { get; set; }
    }
}