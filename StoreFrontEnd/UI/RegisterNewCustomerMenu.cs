using System.Text.RegularExpressions;

namespace UI;
internal class RegisterNewCustomerMenu
{
    private readonly HttpService _httpService;
    public RegisterNewCustomerMenu(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task Start()
    {
        RegisterUser:
        Console.WriteLine("Your username must be 1 to 50 characters long and contain only letters.");
        Console.Write("Type your desired username and press enter: ");
        string? userName = Console.ReadLine();
        string pattern = @"^[a-zA-z]{1,50}$";
        if (userName == null || !Regex.Match(userName, pattern).Success)
        {
            Console.WriteLine("Invalid input.");
            goto RegisterUser;
        }
        
    }
}