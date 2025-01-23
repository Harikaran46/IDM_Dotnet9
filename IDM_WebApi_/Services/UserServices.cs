
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserServices> _logger;

        public UserServices(ApplicationDbContext context, ILogger<UserServices> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> LogIn(User user)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(a => a.ManagerUsername == user.Username && a.ManagerPassword == user.Password);
            if (manager != null)
            {
                _logger.LogInformation($"{manager.ManagerName} logged in successfully.");
                return manager.Id;
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == user.Username && e.Password == user.Password);
            if (employee != null)
            {
                _logger.LogInformation($" {employee.Username} logged in successfully.");
            }
            return employee.EmployeeId;
        }
    }
}

