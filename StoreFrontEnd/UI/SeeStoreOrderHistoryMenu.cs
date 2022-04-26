using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class SeeStoreOrderHistoryMenu
{
    private readonly HttpService _httpService;
    public SeeStoreOrderHistoryMenu(HttpService httpService)
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
        decimal total = 0;
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
                Console.WriteLine("Select a store to see its order history:");
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
                        if (Int32.Parse(input) == store.id)
                        {
                            foreach(Order order in orders)
                            {
                                if(order.storeId == store.id)
                                {
                                    Console.WriteLine("Order: " + order.id + " Date: " + order.datePlaced);
                                    foreach(OrderItem orderItem in orderItems)
                                    {
                                        if (orderItem.orderId == order.id)
                                        {
                                            foreach(Product product in products)
                                            {
                                                if(product.id == orderItem.productId)
                                                {
                                                    Console.WriteLine(product.name + ": " + orderItem.quantity + " X $" + product.price + " = $" + (product.price * orderItem.quantity));
                                                    total += product.price * orderItem.quantity;
                                                }
                                            }
                                        }
                                    }
                                    Console.WriteLine("Total: $" + total);
                                    total = 0;
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