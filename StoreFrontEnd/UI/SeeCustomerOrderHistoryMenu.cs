using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class SeeCustomerOrderHistoryMenu
{
    private readonly HttpService _httpService;
    public SeeCustomerOrderHistoryMenu(HttpService httpService)
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
                foreach(Order order in orders)
                {
                    if(order.userId == currentUser.id)
                    {
                        total = 0;
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
                        exit = true;
                    }
                }
            }
        } while(!exit);
    }
}