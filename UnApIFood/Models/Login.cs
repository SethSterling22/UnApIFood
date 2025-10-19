using System.ComponentModel.DataAnnotations;

namespace UnApIFood.Models
{
    public class Login
    {
        [Required]
        public string? Email { get; set; } = "Email.Prueba@patata.ru";
        [Required]
        public string? Password { get; set; } = "ContraseñaMegaSegura";
    }
}