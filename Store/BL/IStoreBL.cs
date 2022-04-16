using Models;
namespace BL;
public interface IStoreBL
{
    Task<List<Store>> GetStoresAsync();
}
