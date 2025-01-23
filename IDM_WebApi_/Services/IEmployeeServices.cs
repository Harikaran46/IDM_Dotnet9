using MyApi.Models;

namespace IDMApi.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetAllEmployees();
        Task AddEmployee(Employee employee);
        Task<Employee> GetEmployeeProfile(int id);
        Task UpdateEmployeeProfile(int id , Employee employee);
        Task DeleteEmployee(int id);
    }   
}