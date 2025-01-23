
using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

public class TaskServices : ITaskServices
{
    private readonly ApplicationDbContext _context;

    public TaskServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Employee> GetAssignedTask(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        return employee;
    }

    public async Task AssignTask(int id, string task)
    {
        var existingEmployee = await _context.Employees.FindAsync(id);

        if (!string.IsNullOrEmpty(task))
        {
            existingEmployee.Tasks = task;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Task cannot be emty");
        }
    }
}