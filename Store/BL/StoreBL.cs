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
    public async Task<List<User>> GetUsersAsync()
    {
        return await _repo.GetAllUsersAsync();
    }
    public User CreateUser(User userToCreate)
    {
        return _repo.CreateUser(userToCreate);
    }
}