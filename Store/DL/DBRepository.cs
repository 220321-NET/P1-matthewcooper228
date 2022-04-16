using Models;
using Microsoft.Data.SqlClient;
using System.Data;

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
        List<Store> allStores = new List<Store>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Stores", connection);
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