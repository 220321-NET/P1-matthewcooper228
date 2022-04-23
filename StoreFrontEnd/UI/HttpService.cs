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
}