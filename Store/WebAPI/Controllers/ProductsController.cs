using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IStoreBL _bl;

    public ProductsController(IStoreBL bl)
    {
        _bl = bl;
    }
    
    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<List<Product>> GetAsync()
    {
        return await _bl.GetProductsAsync();
    }
}