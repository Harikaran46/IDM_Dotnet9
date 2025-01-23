    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Timesheet
    {
        [Key]
         public int TimesheetId { get; set; } 
         


        [ForeignKey("EmployeeId")] 
        public int EmployeeId { get; set; }



        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }



        [Required(ErrorMessage = "HoursWorked Is Required")] 
        public decimal HoursWorked { get; set; }



        [Required(ErrorMessage ="Task Description Is Required")] 
        public string? TaskDescription { get; set; }

        

        [Required] // Ensuring Status is required
        public string? Status { get; set; }

    }
}
