using System.ComponentModel.DataAnnotations;
namespace MyApi.Models
{
    public class Manager
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$",ErrorMessage ="Name Only Contains Alphapats and Space")]
        public string? ManagerName {get; set;}


        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string? ManagerUsername { get; set; }



        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string? ManagerPassword { get; set; }



        [Required(ErrorMessage = "Department is required.")]
        [StringLength(50, ErrorMessage = "Department cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$",ErrorMessage ="Department Only Contains Alphapats and Space")]
        public string? Department {get; set;}



        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email {get; set;}
    }
}