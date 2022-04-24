using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IStoreBL _bl;

    public OrdersController(IStoreBL bl)
    {
        _bl = bl;
    }
    
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<List<Order>> GetAsync()
    {
        return await _bl.GetOrdersAsync();
    }
}