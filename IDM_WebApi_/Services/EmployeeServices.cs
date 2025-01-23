using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace IDMApi.Services
{
    public class Employeeservices : IEmployeeServices
    {
        private readonly ApplicationDbContext _context;
        public Employeeservices(ApplicationDbContext context)
        {
            _context = context;
        }


        
        public async Task<List<Employee>> GetAllEmployees()
        {
            var allEmployees = await _context.Employees.ToListAsync();
            return allEmployees;
        }

        public async Task AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

        }

        public async Task<Employee> GetEmployeeProfile(int id)
        {
           return await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id); 
        }

        public async Task UpdateEmployeeProfile(int id, Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if(existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Department = employee.Department;
                existingEmployee.Email = employee.Email;
                existingEmployee.Username = employee.Username;
                existingEmployee.Password = employee.Password;
                existingEmployee.MobileNumber = employee.MobileNumber;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.DateOfJoining = employee.DateOfJoining;
                existingEmployee.Tasks = employee.Tasks;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee != null)
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}