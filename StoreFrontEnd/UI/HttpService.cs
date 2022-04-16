using System.Net.Http;
using Models;
using System.Text.Json;

namespace UI;
public class HttpService
{
    private readonly string _apiBaseURL = "https://localhost:7063/api";
    public async Task<List<Store>> GetAllStoresAsync()
    {
        string url = _apiBaseURL + "Stores";
        HttpClient client = new HttpClient();
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.Content.ToString());
        }
        catch(HttpRequestException ex)
        {
            Console.WriteLine("Something bad happened : " + ex);
        }
        
        return new List<Store>();
    }
}