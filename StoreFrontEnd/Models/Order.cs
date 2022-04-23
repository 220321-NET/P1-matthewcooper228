namespace Models;
public class Order
{
    public int id {get; set;}
    public int userId {get; set;}
    public int storeId {get; set;}
    public DateTime datePlaced {get; set;}
}