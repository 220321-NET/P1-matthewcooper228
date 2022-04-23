using Models;
namespace DL;
public interface IRepository
{
    Task<List<Store>> GetAllStoresAsync();
    Task<List<User>> GetAllUsersAsync();
}
