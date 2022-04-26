using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IStoreBL _bl;

    public OrderItemsController(IStoreBL bl)
    {
        _bl = bl;
    }
    
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<List<OrderItem>> GetAsync()
    {
        return await _bl.GetOrderItemsAsync();
    }
    // POST api/<OrderItemsController>
    [HttpPost]
    public ActionResult<OrderItem> Post([FromBody] OrderItem orderItemToCreate)
    {
        OrderItem createdOrderItem = _bl.CreateOrderItem(orderItemToCreate);
        List<OrderItem> orderItems = new List<OrderItem>();
        return Created("api/OrderItems", createdOrderItem);
    }
    [HttpPut]
    public async Task incrementOrderItem(OrderItem orderItem)
    {
        await _bl.incrementOrderItemAsync(orderItem);
    }
}