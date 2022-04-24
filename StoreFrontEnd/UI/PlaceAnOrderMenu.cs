using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class PlaceAnOrderMenu
{
    private readonly HttpService _httpService;
    public PlaceAnOrderMenu(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task Start()
    {
        List<InventoryItem> inventoryItems = await _httpService.GetAllInventoryItemsAsync();
        List<Order> orders = await _httpService.GetAllOrdersAsync();
        List<OrderItem> orderItems = await _httpService.GetAllOrderItemsAsync();
        List<Product> products = await _httpService.GetAllProductsAsync();
        List<Store> stores = await _httpService.GetAllStoresAsync();
        List<User> users = await _httpService.GetAllUsersAsync();
        Order currentOrder = new Order();
        Store currentStore = new Store();
        User currentUser = new User();

        

        Login:
        Console.Write("Enter your username and press enter: ");
        string? userName = Console.ReadLine();
        string pattern = @"^[a-zA-z]{1,50}$";
        if (userName == null || !Regex.Match(userName, pattern).Success)
        {
            Console.WriteLine("Invalid input.");
            goto Login;
        }
        bool thereIsAnExistingAccount = false;
        foreach(User user in users)
        {
            if (userName == user.name)
            {
                thereIsAnExistingAccount = true;
                currentUser = user;
            }
        }
        if(!thereIsAnExistingAccount)
        {
            Console.WriteLine("There is no existing account for that username. Please create an account ");
        }
        else
        {
            ChooseAStore:
            Console.WriteLine("Which store would like to place an order with?");
            foreach(Store store in stores)
            {
                Console.WriteLine("[" + store.id + "] " + store.address);
            }
            Console.Write("Type a number or x and press enter: ");
            string? storeIdInput = Console.ReadLine();
            string storeIdPattern = @"^[0-9]+$";
            if (storeIdInput == null || !Regex.Match(storeIdInput, storeIdPattern).Success)
            {
                Console.WriteLine("Invalid input.");
                goto ChooseAStore;
            }
            
        }
    }
}