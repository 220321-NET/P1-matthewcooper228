using Models;
using DL;
namespace BL;
public interface IStoreBL
{
    Task<List<Store>> GetStoresAsync();
    Task<List<User>> GetUsers();
}
