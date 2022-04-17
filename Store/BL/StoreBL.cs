using DL;
using Models;
namespace BL;
public class StoreBL : IStoreBL
{
    private readonly IRepository _repo;
    public StoreBL(IRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Store>> GetStoresAsync()
    {
        return await _repo.GetAllStoresAsync();
    }
}