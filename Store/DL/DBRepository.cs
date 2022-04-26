using Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Serilog;

namespace DL;
public class DBRepository : IRepository
{   
    private readonly string _connectionString;
    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<List<InventoryItem>> GetAllInventoryItemsAsync()
    {
        Log.Information("A request was made to see get the inventory items.");   
        List<InventoryItem> allInventoryItems = new List<InventoryItem>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM InventoryItems", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            int storeId = reader.GetInt32(1);
            int productId = reader.GetInt32(2);
            int quantity = reader.GetInt32(3);
            InventoryItem inventoryItem = new InventoryItem{
                Id = id,
                StoreId = storeId,
                ProductId = productId,
                Quantity = quantity
            };
            allInventoryItems.Add(inventoryItem);
        }
        reader.Close();
        connection.Close();
        return allInventoryItems;
    }
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        Log.Information("A request was made to get the orders.");   
        List<Order> allOrders = new List<Order>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM Orders", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            int userId = reader.GetInt32(1);
            int storeId = reader.GetInt32(2);
            DateTime datePlaced = reader.GetDateTime(3);
            Order order = new Order{
                Id = id,
                UserId = userId,
                StoreId = storeId,
                DatePlaced = datePlaced
            };
            allOrders.Add(order);
        }
        reader.Close();
        connection.Close();
        return allOrders;
    }
    public async Task<List<OrderItem>> GetAllOrderItemsAsync()
    {
        Log.Information("A request was made to get the order items.");   
        List<OrderItem> allOrderItems = new List<OrderItem>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM OrderItems", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            int orderId = reader.GetInt32(1);
            int productId = reader.GetInt32(2);
            int quantity = reader.GetInt32(3);
            OrderItem orderItem = new OrderItem{
                Id = id,
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            };
            allOrderItems.Add(orderItem);
        }
        reader.Close();
        connection.Close();
        return allOrderItems;
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        Log.Information("A request was made to get the products.");   
        List<Product> allProducts = new List<Product>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM Products", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            decimal price = reader.GetDecimal(2);
            Product product = new Product{
                Id = id,
                Name = name,
                Price = price
            };
            allProducts.Add(product);
        }
        reader.Close();
        connection.Close();
        return allProducts;
    }
    public async Task<List<Store>> GetAllStoresAsync()
    {
        Log.Information("A request was made to see the store locations.");   
        List<Store> allStores = new List<Store>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM Stores", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string address = reader.GetString(1);
            Store store = new Store{
                Id = id,
                Address = address
            };
            allStores.Add(store);
        }
        reader.Close();
        connection.Close();
        return allStores;
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        Log.Information("A request was made to get all Users.");   
        List<User> allUsers = new List<User>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("Select * FROM Users", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            bool isEmployee = reader.GetBoolean(2);
            User user = new User{
                Id = id,
                Name = name,
                IsEmployee = isEmployee
            };
            allUsers.Add(user);
        }
        reader.Close();
        connection.Close();
        return allUsers;
    }
    public User CreateUser(User userToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Users(Name, IsEmployee) OUTPUT INSERTED.Id VALUES (@name, @isEmployee)", connection);

        cmd.Parameters.AddWithValue("@name", userToCreate.Name);
        cmd.Parameters.AddWithValue("@isEmployee", userToCreate.IsEmployee);

        try
        {
            userToCreate.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        connection.Close();
        
        return userToCreate;
    }
    public Order CreateOrder(Order orderToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Orders(UserId, StoreId, DatePlaced) OUTPUT INSERTED.Id VALUES (@userId, @storeId, @datePlaced)", connection);

        cmd.Parameters.AddWithValue("@userId", orderToCreate.UserId);
        cmd.Parameters.AddWithValue("@storeId", orderToCreate.StoreId);
        cmd.Parameters.AddWithValue("@datePlaced", orderToCreate.DatePlaced);

        try
        {
            orderToCreate.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        connection.Close();
        
        return orderToCreate;
    }
    public OrderItem CreateOrderItem(OrderItem orderItemToCreate)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("INSERT INTO OrderItems(OrderId, ProductId, Quantity) OUTPUT INSERTED.Id VALUES (@orderId, @productId, @quantity)", connection);
        cmd.Parameters.AddWithValue("@orderId", orderItemToCreate.OrderId);
        cmd.Parameters.AddWithValue("@productId", orderItemToCreate.ProductId);
        cmd.Parameters.AddWithValue("@quantity", orderItemToCreate.Quantity);
        try
        {
            orderItemToCreate.Id = (int) cmd.ExecuteScalar();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        connection.Close();
        return orderItemToCreate;
    }
    public async Task decrementInventoryItemAsync(InventoryItem inventoryItem)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand update = new SqlCommand("UPDATE InventoryItems SET Quantity = Quantity - 1 WHERE Id = @inventoryItemId", connection);
        update.Parameters.AddWithValue("@inventoryItemId", inventoryItem.Id);
        await update.ExecuteNonQueryAsync();
        connection.Close();
    }
    public async Task incrementOrderItemAsync(OrderItem orderItem)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand update = new SqlCommand("UPDATE OrderItems SET Quantity = Quantity + 1 WHERE Id = @orderItemId", connection);
        update.Parameters.AddWithValue("@orderItemId", orderItem.Id);
        await update.ExecuteNonQueryAsync();
        connection.Close();
    }
        public async Task replenishStoreInventoryAsync(Store store)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand update = new SqlCommand("UPDATE InventoryItems SET Quantity = 10 WHERE StoreId = @storeId", connection);
        update.Parameters.AddWithValue("@storeId", store.Id);
        await update.ExecuteNonQueryAsync();
        connection.Close();
    }

}