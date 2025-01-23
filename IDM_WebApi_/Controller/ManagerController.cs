using System.Data;
using Dapper;
using IDMApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly IManagerService _managerService;
        private readonly ITaskServices _taskServices;
        private readonly string _connectionstring; //to retrieve the connection string.
        private readonly ILogger<ManagerController> _logger;

        public ManagerController(
            ILogger<ManagerController> logger,
            IEmployeeServices employeeServices,
            IManagerService managerService,
            ITaskServices taskServices)
        {
            _managerService = managerService;
            _employeeServices = employeeServices;
            _taskServices = taskServices;
            _logger = logger;
        }


        // GET: api/Manager/allEmployees
        [HttpGet("getallemployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeServices.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = "An error occurred while fetching employees.", error = exception.Message });
            }
        }


        [HttpGet("getallmanagers")]
        public async Task<IActionResult> GetAllManagers()
        {
            try
            {
                var allManagers = await _managerService.GetAllManager();

                return Ok(allManagers);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = "An error occurred while fetching employees.", error = exception.Message });
            }
        }

        // POST: api/Manager/addEmployee
        [HttpPost("addemployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest(new { message = "Employee data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _employeeServices.AddEmployee(employee);

            return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.EmployeeId }, employee);
        }


        [HttpPost("addmanager")]
        public async Task<IActionResult> AddManager([FromBody] Manager manager)
        {
            if (manager == null)
            {
                return BadRequest(new { message = "Manager data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _managerService.AddManager(manager);

            return CreatedAtAction(nameof(GetAllEmployees), new { id = manager.Id }, manager);
        }



        // GET: api/Manager/getAssignedTask/{id}
        [HttpGet("getassignedtask/{id}")]
        public async Task<IActionResult> GetAssignedTaskAsync(int id)
        {
            try
            {
                var task = await _taskServices.GetAssignedTask(id);

                if (task == null)
                {
                    return NotFound(new { message = "Task not found for this employee." });
                }
                return Ok(task);
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the task.", error = exception.Message });
            }
        }

        // PATCH: api/Manager/assignTask/{id}
        [HttpPatch("assigntask/{id}")]
        public async Task<IActionResult> AssignTask(int id, string task)
        {
            await _taskServices.AssignTask(id, task);
            return Ok(new{message ="Task Assigned Successfully"});
        }

        // GET: api/Manager/getEmployeeProfile/{id}
        [HttpGet("getemployeeprofile/{id}")]
        public async Task<IActionResult> GetEmployeeProfile(int id)
        {
            var employee = await _employeeServices.GetEmployeeProfile(id);
            if (employee == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            return Ok(employee);
        }

        [HttpGet("getmanagerprofile/{id}")]
        public async Task<IActionResult> GetManagerProfile(int id)
        {
            var manager = await _managerService.GetManagerProfile(id);
            if (manager == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            return Ok(manager);
        }

        // PUT: api/Manager/editProfile/{id}
        [HttpPut("updateemployeeprofile/{id}")]
        public async Task<IActionResult> UpdateEmployeeProfile(int id, [FromBody] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest(new { message = "Employee ID mismatch." });
            }

            if (ModelState.IsValid)
            {
                await _employeeServices.UpdateEmployeeProfile(id, employee);
                return Ok(new { message = "Employee updated successfully" });
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Manager/deleteEmployee/{id}
        [HttpDelete("deleteemployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeServices.DeleteEmployee(id);
            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
