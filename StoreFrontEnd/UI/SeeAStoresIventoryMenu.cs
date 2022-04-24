using System.Text.RegularExpressions;
using Models;

namespace UI;
internal class SeeAStoresInventoryMenu
{
    private readonly HttpService _httpService;
    public SeeAStoresInventoryMenu(HttpService httpService)
    {
        _httpService = httpService;
    }
    public async Task Start()
    {
        List<Store> stores = await _httpService.GetAllStoresAsync();
        List<Product> products = await _httpService.GetAllProductsAsync();
        List<InventoryItem> inventoryItems = await _httpService.GetAllInventoryItemsAsync();
        bool exit = false;
        while(!exit)
        {
            ChooseAStore:
            Console.WriteLine("Select a store to see its inventory:");
            foreach(Store store in stores)
            {
                Console.WriteLine("[" + store.id + "] " + store.address);
            }
            Console.Write("Type a number or x and press enter: ");
            string? input = Console.ReadLine();
            string pattern = @"^[0-9]+$";
            if (input == null || !Regex.Match(input, pattern).Success)
            {
                Console.WriteLine("Invalid input.");
                goto ChooseAStore;
            }
            Store chosenStore = new Store(){
                id = -1,
                address = ""
            };
            foreach(Store store in stores)
            {
                if (Int32.Parse(input) == store.id)
                {
                    chosenStore.id = store.id;
                    chosenStore.address = store.address;
                }
            }
            Console.WriteLine("Here is the inventory at store " + chosenStore.id + " (" + chosenStore.address + "):");
            foreach(InventoryItem inventoryItem in inventoryItems)
            {
                foreach(Product product in products)
                {
                    if (inventoryItem.storeId == chosenStore.id && inventoryItem.productId == product.id)
                    {
                        Console.WriteLine("[" + inventoryItem.id + "] " + product.name + " price: $" + product.price + " stock: " + inventoryItem.quantity);
                    }
                }
            }
            exit = true;
        }
    }
}