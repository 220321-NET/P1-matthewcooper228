using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class CheckIfCustomerExistsMenu
{
    private readonly HttpService _httpService;
    public CheckIfCustomerExistsMenu(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task Start()
    {
        CheckUserName:
        Console.WriteLine("A valid username must be 1 to 50 characters long and contain only letters.");
        Console.Write("Type a username to check and then press enter: ");
        string? userName = Console.ReadLine();
        string pattern = @"^[a-zA-z]{1,50}$";
        if (userName == null || !Regex.Match(userName, pattern).Success)
        {
            Console.WriteLine("Invalid input.");
            goto CheckUserName;
        }
        List<User> users = await _httpService.GetAllUsersAsync();
        bool thereIsAnExistingAccount = false;
        foreach(User user in users)
        {
            if (userName == user.name)
            {
                thereIsAnExistingAccount = true;
            }
        }
        if(thereIsAnExistingAccount)
        {
            Console.WriteLine("There is an existing account for " + userName + ".");
        }
        else
        {
            Console.WriteLine("There is no existing account for " + userName + ".");
        }
    }
}