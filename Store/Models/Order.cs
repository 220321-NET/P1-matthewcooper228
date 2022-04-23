namespace Models;
public class Order
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int StoreId {get; set;}
    public DateTime DatePlaced {get; set;}
}