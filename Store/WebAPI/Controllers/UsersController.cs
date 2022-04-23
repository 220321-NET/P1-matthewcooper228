using Microsoft.AspNetCore.Mvc;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IStoreBL _bl;

    public UsersController(IStoreBL bl)
    {
        _bl = bl;
    }
    
    // GET: api/<UsersController>
    [HttpGet]
    public async Task<List<User>> GetAsync()
    {
        return await _bl.GetUsersAsync();
    }
    // POST api/<UsersController>
    [HttpPost]
    public ActionResult<User> Post([FromBody] User userToCreate)
    {
        User createdUser = _bl.CreateUser(userToCreate);
        List<User> users = new List<User>();
        return Created("api/Users", createdUser);
    }

}