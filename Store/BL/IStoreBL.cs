using Models;
using DL;
namespace BL;
public interface IStoreBL
{
    Task<List<InventoryItem>> GetInventoryItemsAsync();
    Task<List<Order>> GetOrdersAsync();
    Task<List<OrderItem>> GetOrderItemsAsync();
    Task<List<Product>> GetProductsAsync();
    Task<List<Store>> GetStoresAsync();
    Task<List<User>> GetUsersAsync();
    User CreateUser(User userToCreate);
    Order CreateOrder(Order orderToCreate);
    OrderItem CreateOrderItem(OrderItem orderItemToCreate);
    public Task decrementInventoryItemAsync(InventoryItem inventoryItem);
    public Task incrementOrderItemAsync(OrderItem orderItem);
    public Task replenishStoreInventoryAsync(Store store);
}
