using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class ReplenishStoreInventoryMenu
{
    private readonly HttpService _httpService;
    public ReplenishStoreInventoryMenu(HttpService httpService)
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
        User currentUser = new User();
        Store currentStore = new Store();
        bool exit = false;
        do
        {
            Login:
            Console.Write("Enter your employee username and press enter: ");
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
                if (userName == user.name && user.isEmployee == true)
                {
                    thereIsAnExistingAccount = true;
                    currentUser = user;
                }
            }
            if(!thereIsAnExistingAccount)
            {
                Console.WriteLine("There is no existing employee account for that username.");
                exit = true;
            }
            else
            {
                ChooseAStore:
                Console.WriteLine("Select a store to see its inventory:");
                foreach(Store store in stores)
                {
                    Console.WriteLine("[" + store.id + "] " + store.address);
                }
                Console.Write("Type a number or x and press enter: ");
                string? input = Console.ReadLine();
                string storeIdPattern = @"^[0-9]+$";
                if (input == null || !Regex.Match(input, storeIdPattern).Success)
                {
                    Console.WriteLine("Invalid input.");
                    goto ChooseAStore;
                }
                else if (input == "x" || input == "X")
                {
                    exit = true;
                }
                else
                {
                    foreach(Store store in stores)
                    {
                        if(Int32.Parse(input) == store.id)
                        {
                            currentStore = store;
                            Console.WriteLine("Here is the inventory at store " + store.id + " (" + store.address + "):");
                            foreach(InventoryItem inventoryItem in inventoryItems)
                            {
                                if(inventoryItem.storeId == store.id)
                                {
                                    foreach(Product product in products)
                                    {
                                        if(product.id == inventoryItem.productId && inventoryItem.storeId == store.id)
                                        {
                                        Console.WriteLine("[" + inventoryItem.id + "] " + product.name + " price: $" + product.price + " stock: " + inventoryItem.quantity);
                                        }
                                    }
                                }
                            }
                            ChooseToReplenish:
                            Console.WriteLine("Do you want to replenish the inventory?");
                            Console.WriteLine("[Y] Yes, I want to replenish the inventory.");
                            Console.WriteLine("[X] No, I want to go back.");
                            Console.Write("Type Y or X and press enter: ");
                            string? replenishInput = Console.ReadLine();
                            string replenishPattern = @"^[xXyY]$";
                            if (replenishInput == "x" || replenishInput == "X") exit = true;
                            else if (replenishInput == null || !Regex.Match(replenishInput, replenishPattern).Success)
                            {
                                Console.WriteLine("Invalid input.");
                                goto ChooseToReplenish;
                            }
                            else
                            {
                                await _httpService.replenishStoreInventoryAsync(currentStore);
                                inventoryItems = await _httpService.GetAllInventoryItemsAsync();
                                Console.WriteLine("Here is the replenished inventory at store " + store.id + " (" + store.address + "):");
                                foreach(InventoryItem inventoryItem in inventoryItems)
                                {
                                    if(inventoryItem.storeId == store.id)
                                    {
                                        foreach(Product product in products)
                                        {
                                            if(product.id == inventoryItem.productId && inventoryItem.storeId == store.id)
                                            {
                                            Console.WriteLine("[" + inventoryItem.id + "] " + product.name + " price: $" + product.price + " stock: " + inventoryItem.quantity);
                                            }
                                        }
                                    }
                                }
                                exit = true;
                            }
                        }
                    }
                }                
            }
        } while(!exit);
    }
}