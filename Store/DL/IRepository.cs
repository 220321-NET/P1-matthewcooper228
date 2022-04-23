using Models;
namespace DL;
public interface IRepository
{
    Task<List<InventoryItem>> GetAllInventoryItemsAsync();
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<OrderItem>> GetAllOrderItemsAsync();
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Store>> GetAllStoresAsync();
    Task<List<User>> GetAllUsersAsync();
    User CreateUser(User userToCreate);
}
