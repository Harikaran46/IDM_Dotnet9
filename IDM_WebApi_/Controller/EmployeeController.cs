using System.Threading.Tasks;
using IDMApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Data;

namespace MyApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        
        private readonly IEmployeeServices _employeeServices;
        private readonly ITaskServices _taskServices;

        public EmployeesController(IEmployeeServices employeeServices,ITaskServices taskServices)
        {
            
            _employeeServices = employeeServices;
            _taskServices = taskServices;

        }


        // Route: GET api/employees/profile/{id}
        [HttpGet("getEmployeeprofile/{id}")]
        public async Task<IActionResult> GetEmployeeProfile(int id)
        {
            
            var employee = await _employeeServices.GetEmployeeProfile(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }
            return Ok(employee);
        }

        // Route: GET api/employees/task/{id}
        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetAssignedTask(int id)
        {
            var task = await _taskServices.GetAssignedTask(id);
            if(task == null)
            {
                return NotFound(new { message = "No found" });
            }
            return Ok(new {employeeId = id,task});
        }
    }
}
