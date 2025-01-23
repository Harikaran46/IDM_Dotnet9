using MyApi.Models;

namespace IDMApi.Services
{
    public interface IManagerService
    {
        Task<List<Manager>> GetAllManager();

        Task AddManager(Manager manager);
        Task<Manager> GetManagerProfile(int id);
    }
}           