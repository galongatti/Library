using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("employers")]
    public async Task<IActionResult> CreateEmployer([FromBody] CreateUserEmployer model)
    {
        User user = await userService.CreateEmployerAsync(model);
        return Ok(user);
    }

    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateUserCustomer model)
    {
        User user = await userService.CreateCustomerAsync(model);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<User> users = await userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUsername([FromRoute] string username)
    {
        User? user = await userService.GetUserByUsernameAsync(username);
        if (user is null) return NotFound();
        return Ok(user);
    }
}

