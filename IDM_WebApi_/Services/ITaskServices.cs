using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

public interface ITaskServices
{
    Task<Employee> GetAssignedTask(int id);
    Task AssignTask(int id,string task);
    

}