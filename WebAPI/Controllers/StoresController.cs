using Microsoft.AspNetCore.Mvc;
using BL;
using Models;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreBL _bl;
        public StoresController(IStoreBL bl)
        {
            _bl = bl;
        }
        // GET: api/<StoresController>
        [HttpGet]
        public List<Store> Get()
        {
            return _bl.GetStores();
        }
    }
}