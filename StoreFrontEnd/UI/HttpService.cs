using Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace UI;
public class HttpService
{
    private readonly string _apiBaseURL = "https://localhost:7255/api/";
    private HttpClient client = new HttpClient();
    public HttpService()
    {
        client.BaseAddress = new Uri(_apiBaseURL);
    }
    public async Task<List<InventoryItem>> GetAllInventoryItemsAsync()
    {
        List<InventoryItem> inventoryItems = new List<InventoryItem>();
        string url = _apiBaseURL + "InventoryItems";
        try
        {
            inventoryItems = await JsonSerializer.DeserializeAsync<List<InventoryItem>>(await client.GetStreamAsync("InventoryItems")) ?? new List<InventoryItem>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return inventoryItems;
    }
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        List<Order> orders = new List<Order>();
        string url = _apiBaseURL + "Orders";
        try
        {
            orders = await JsonSerializer.DeserializeAsync<List<Order>>(await client.GetStreamAsync("Orders")) ?? new List<Order>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return orders;
    }
    public async Task<List<OrderItem>> GetAllOrderItemsAsync()
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        string url = _apiBaseURL + "OrderItems";
        try
        {
            orderItems = await JsonSerializer.DeserializeAsync<List<OrderItem>>(await client.GetStreamAsync("OrderItems")) ?? new List<OrderItem>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return orderItems;
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        List<Product> products = new List<Product>();
        string url = _apiBaseURL + "Products";
        try
        {
            products = await JsonSerializer.DeserializeAsync<List<Product>>(await client.GetStreamAsync("Products")) ?? new List<Product>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return products;
    }
    public async Task<List<Store>> GetAllStoresAsync()
    {
        List<Store> stores = new List<Store>();
        string url = _apiBaseURL + "Store";
        try
        {
            stores = await JsonSerializer.DeserializeAsync<List<Store>>(await client.GetStreamAsync("Stores")) ?? new List<Store>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return stores;
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> users = new List<User>();
        string url = _apiBaseURL + "Users";
        try
        {
            users = await JsonSerializer.DeserializeAsync<List<User>>(await client.GetStreamAsync("Users")) ?? new List<User>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return users;
    }
    public async Task<User> CreateUserAsync(User userToCreate)
    {
        string serializedUser = JsonSerializer.Serialize(userToCreate);
        StringContent content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PostAsync("Users", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<User>(await response.Content.ReadAsStreamAsync()) ?? new User();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task<Order> CreateOrderAsync(Order orderToCreate)
    {
        string serializedOrder = JsonSerializer.Serialize(orderToCreate);
        StringContent content = new StringContent(serializedOrder, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PostAsync("Orders", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<Order>(await response.Content.ReadAsStreamAsync()) ?? new Order();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItemToCreate)
    {
        string serializedOrderItem = JsonSerializer.Serialize(orderItemToCreate);
        StringContent content = new StringContent(serializedOrderItem, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PostAsync("OrderItems", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<OrderItem>(await response.Content.ReadAsStreamAsync()) ?? new OrderItem();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task<OrderItem> IncrementOrderItem(OrderItem updatedOrderItem)
    {
        string serializedUpdatedOrderItem = JsonSerializer.Serialize(updatedOrderItem);
        StringContent content = new StringContent(serializedUpdatedOrderItem, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PutAsync("OrderItems", content);
            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<OrderItem>(await response.Content.ReadAsStreamAsync()) ?? new OrderItem();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task decrementInventoryItemAsync(InventoryItem inventoryItem)
    {
        string serializedInventoryItem = JsonSerializer.Serialize(inventoryItem);
        StringContent content = new StringContent(serializedInventoryItem, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PutAsync("InventoryItems", content);
            response.EnsureSuccessStatusCode();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task incrementOrderItemAsync(OrderItem orderItem)
    {
        string serializedOrderItem = JsonSerializer.Serialize(orderItem);
        StringContent content = new StringContent(serializedOrderItem, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PutAsync("OrderItems", content);
            response.EnsureSuccessStatusCode();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
    public async Task replenishStoreInventoryAsync(Store store)
    {
        string serializedStore = JsonSerializer.Serialize(store);
        StringContent content = new StringContent(serializedStore, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await client.PutAsync("Stores", content);
            response.EnsureSuccessStatusCode();
        }
        catch(HttpRequestException)
        {
            throw;
        }
    }
}