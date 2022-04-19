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
            //HttpResponseMessage response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            //string responseString = await response.Content.ReadAsStringAsync();
            //string responseString = await client.GetStringAsync(url);
            //stores = JsonSerializer.Deserialize<List<Store>>(responseString) ?? new List<Store>();
            stores = await JsonSerializer.DeserializeAsync<List<Store>>(await client.GetStreamAsync("Store")) ?? new List<Store>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened: " + ex);
        }
        return stores;
    }

}