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
}