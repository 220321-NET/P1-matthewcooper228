using Models;
namespace UI;

internal class MainMenu
{
    private readonly HttpService _httpService;
    public MainMenu(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task Start()
    {
        bool exit = false;
        while(!exit)
        {
        Console.WriteLine("Store:");
        Console.WriteLine("[1] I am a new customer and I want to register.");
        Console.WriteLine("[2] I want to see if I have an existing customer account.");
        Console.WriteLine("[3] I want to look at a store's inventory.");
        Console.WriteLine("[4] I am an existing customer and I want to see my order history from latest to oldest.");
        Console.WriteLine("[5] I am an existing customer and I want to see my order history from oldest to latest.");
        Console.WriteLine("[6] I am an existing customer and I want to see my order history from least to most expensive.");
        Console.WriteLine("[7] I am an existing customer and I want to see my order history from most to least expensive.");
        Console.WriteLine("[8] I am an existing customer and I want to place an order to a store location.");
        Console.WriteLine("[9] I am an employee and I want to see order history of a store location.");
        Console.WriteLine("[10] I am an employee and I want to replenish the the inventory of a store location.");
        Console.WriteLine("[X] I want to exit the program.");
        Console.WriteLine("Type a number or x and press enter: ");
        string? input = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(input))
            {
                switch(input)
                {
                    case "1":
                        await new RegisterNewCustomerMenu(_httpService).Start();
                    break;
                    case "2":
                        await new CheckIfCustomerExistsMenu(_httpService).Start();
                    break;
                    case "3":
                        await new SeeAStoresInventoryMenu(_httpService).Start();
                    break;
                    case "x":
                    case "X":
                        exit = true;
                    break;
                }
            }
        }
    }
}