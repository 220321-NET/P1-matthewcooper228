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
    public async Task<List<InventoryItem>> GetInventoryItemsAsync()
    {
        return await _repo.GetAllInventoryItemsAsync();
    }
    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _repo.GetAllOrdersAsync();
    }
    public async Task<List<OrderItem>> GetOrderItemsAsync()
    {
        return await _repo.GetAllOrderItemsAsync();
    }
    public async Task<List<Product>> GetProductsAsync()
    {
        return await _repo.GetAllProductsAsync();
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
    public Order CreateOrder(Order orderToCreate)
    {
        return _repo.CreateOrder(orderToCreate);
    }
    public OrderItem CreateOrderItem(OrderItem orderItemToCreate)
    {
        return _repo.CreateOrderItem(orderItemToCreate);
    }
    public async Task decrementInventoryItemAsync(InventoryItem inventoryItem)
    {
        await _repo.decrementInventoryItemAsync(inventoryItem);
    }
    public async Task incrementOrderItemAsync(OrderItem orderItem)
    {
        await _repo.incrementOrderItemAsync(orderItem);
    }
    public async Task replenishStoreInventoryAsync(Store store)
    {
        await _repo.replenishStoreInventoryAsync(store);
    }

}