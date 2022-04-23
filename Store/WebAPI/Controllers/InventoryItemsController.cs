using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryItemsController : ControllerBase
{
    private readonly IStoreBL _bl;

    public InventoryItemsController(IStoreBL bl)
    {
        _bl = bl;
    }
    
    // GET: api/<InventoryItemsController>
    [HttpGet]
    public async Task<List<InventoryItem>> GetAsync()
    {
        return await _bl.GetInventoryItemsAsync();
    }
}