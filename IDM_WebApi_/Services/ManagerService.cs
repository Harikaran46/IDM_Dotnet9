using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
namespace IDMApi.Services
{

    public class ManagerService : IManagerService
    {
        private readonly ApplicationDbContext _context;
        public ManagerService( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manager> GetManagerProfile(int id)
        {
            return await _context.Managers.FirstOrDefaultAsync(manager => manager.Id == id);
        }

        public async Task<List<Manager>> GetAllManager()
        {
            var allManagers = await _context.Managers.ToListAsync();
            return allManagers;
        }

        public async Task AddManager(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
        }   
        
    }
}