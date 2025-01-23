using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string? Username { get; set; }

    
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        public string? Password { get; set; }
    }

    
}