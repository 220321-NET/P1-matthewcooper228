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
        Store currentStore = new Store();
        User currentUser = new User();
        bool exitOrderMenu = false;
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
                ChooseAStore:
                Console.WriteLine("Which store would like to place an order with?");
                foreach(Store store in stores)
                {
                    Console.WriteLine("[" + store.id + "] " + store.address);
                }
                Console.WriteLine("[X] I want to go back.");
                Console.Write("Type a number or x and press enter: ");
                string? storeIdInput = Console.ReadLine();
                string storeIdPattern = @"^[0-9]+$";
                if (storeIdInput == "x" || storeIdInput == "X")
                {
                    Console.WriteLine("X if block has been reached.");
                    exitOrderMenu = true;
                }
                else if (storeIdInput == null || !Regex.Match(storeIdInput, storeIdPattern).Success)
                {
                    Console.WriteLine("Invalid input.");
                    goto ChooseAStore;
                }
                else
                {
                    ChooseToStartAnOrder:
                    foreach (Store store in stores)
                    {
                        if (store.id == Int32.Parse(storeIdInput))
                        {
                            Console.WriteLine("Here are the products available at " + store.address);
                        }
                        foreach (Product product in products)
                        {
                            foreach (InventoryItem inventoryItem in inventoryItems)
                            {
                                if (
                                    inventoryItem.productId == product.id &&
                                    store.id == inventoryItem.storeId &&
                                    Int32.Parse(storeIdInput) == store.id
                                )
                                {
                                    Console.WriteLine(product.name + " price: $" + product.price + " stock: " + inventoryItem.quantity);
                                }
                            }
                        }
                    }
                    Console.WriteLine("Do you want to start an order at this location?");
                    Console.WriteLine("[Y] Yes, I want to start an order.");
                    Console.WriteLine("[X] No, I don't want to start an order and I want to go back.");
                    Console.Write("Type Y or X and press enter: ");
                    string? startAnOrderInput = Console.ReadLine();
                    string startAnOrderPattern = @"^[xXyY]$";
                    if (startAnOrderInput == "x" || startAnOrderInput == "X") exitOrderMenu = true;
                    else if (startAnOrderInput == null || !Regex.Match(startAnOrderInput, startAnOrderPattern).Success)
                    {
                        Console.WriteLine("Invalid input.");
                        goto ChooseToStartAnOrder;
                    }
                    else
                    {
                        foreach(Store store in stores)
                        {
                            if (Int32.Parse(storeIdInput) == store.id)
                            {
                                currentStore = store;
                            }
                        }
                        Order currentOrder = new Order(){
                            userId = currentUser.id,
                            storeId = currentStore.id,
                            datePlaced = DateTime.Now
                        };
                        Order placedOrder = await _httpService.CreateOrderAsync(currentOrder);
                        ChooseAnItemToAddToOrder:
                        Console.WriteLine("What would you like to add to your order?");
                        foreach (Store store in stores)
                        {
                            foreach (Product product in products)
                            {
                                foreach (InventoryItem inventoryItem in inventoryItems)
                                {
                                    if (
                                        inventoryItem.productId == product.id &&
                                        store.id == inventoryItem.storeId &&
                                        placedOrder.storeId == store.id
                                    )
                                    {
                                        Console.WriteLine("[" + inventoryItem.id + "]" + product.name + " price: $" + product.price + " stock: " + inventoryItem.quantity);
                                    }
                                }
                            }
                        }
                        Console.Write("Type a number to add one of these items to your order or X to finish your order and go back: ");
                        string? inventoryItemInput = Console.ReadLine();
                        string inventoryItemPattern = @"^[0-9]+$";
                        if (inventoryItemInput == "x" || inventoryItemInput == "X")
                        {
                            Console.WriteLine("Your order has been placed.");
                            exitOrderMenu = true;
                        }
                        else if (inventoryItemInput == null || !Regex.Match(inventoryItemInput, inventoryItemPattern).Success)
                        {
                        Console.WriteLine("Invalid input.");
                        goto ChooseAnItemToAddToOrder;
                        }
                        else
                        {
                            // check if this order item already exists
                            InventoryItem currentInventoryItem = new InventoryItem();
                            foreach(InventoryItem inventoryItem in inventoryItems)
                            {
                                if(Int32.Parse(inventoryItemInput) == inventoryItem.id)
                                {
                                    currentInventoryItem = inventoryItem;
                                }
                            }
                            foreach(OrderItem orderItem in orderItems)
                            {
                                if(currentInventoryItem.productId == orderItem.productId)
                                {
                                    // the order item already exists so increment quantity
                                    Console.WriteLine("This item is already in your order");
                                }
                            }
                            // the order item does not exist, create a new order item with quantity 1
                            OrderItem orderItemToCreate = new OrderItem(){
                                orderId = placedOrder.id,
                                productId = currentInventoryItem.productId,
                                quantity = 1
                            };
                            OrderItem createdOrderItem = await _httpService.CreateOrderItemAsync(orderItemToCreate);
                            // decrement the inventory item by 1
                        }
                    }
                }
            }
        } while(!exitOrderMenu);
    }
}