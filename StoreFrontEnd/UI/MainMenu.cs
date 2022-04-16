namespace UI;

internal class MainMenu
{
    public MainMenu() {}
    public async Task Start()
    {
        Console.Write("Type your input and then press enter: ");
        Console.ReadLine();
        await DisplayAllStoresAsync();
    }
    private async Task DisplayAllStoresAsync()
    {
        Console.WriteLine("Here are all the stores");
        await new HttpService().GetAllStoresAsync();
    }
}