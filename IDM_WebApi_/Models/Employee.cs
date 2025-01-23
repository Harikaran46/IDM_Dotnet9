using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyApi.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Department is required.")]
        [StringLength(50, ErrorMessage = "Department cannot exceed 50 characters.")]
        public required string Department { get; set; }


        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public required string Username { get; set; }


        [Required(ErrorMessage = "Password is required.")]
         [StringLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]

       public string Password {get; set;}


        [Required(ErrorMessage = "Task is required.")]
        [StringLength(50, ErrorMessage = "Task cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z ]+$",ErrorMessage ="Task Only Contains Alphapats and Space")]
        public string Tasks { get; set; } 


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid mobile number.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number must be 10 digits.")]
        [RegularExpression(@"^[7-9]\d{9}$", ErrorMessage = "Mobile number must be 10 digits and start with 7, 8, or 9.")]
        public string MobileNumber { get; set; }


        
        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date of Birth.")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Date of Joining is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date of Joining.")]
        public DateTime DateOfJoining { get; set; }
    }
}