using MyApi.Models;

public interface IUserServices
{
    Task<int> LogIn(User user);

}