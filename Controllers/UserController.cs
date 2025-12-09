using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("internal-users")]
    public async Task<IActionResult> CreateInternalUser([FromBody] CreateUser model)
    {
        User user = await userService.CreateInternalUserAsync(model);
        
        ReadUser readUser = ReadUser.FromUser(user);
        
        return CreatedAtAction(nameof(GetByUserName), new { username = user.UserName }, readUser);
    }

    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateUser model)
    {
        User user = await userService.CreateCustomerAsync(model);
        
        ReadUser readUser = ReadUser.FromUser(user);
        return CreatedAtAction(nameof(GetByUserName), new { username = user.UserName }, readUser);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<User> users = await userService.GetUsersAsync();

        List<ReadUser> readUsers = ReadUser.FromUsers(users);
        
        return Ok(readUsers);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        User? user = await userService.GetUserByUsernameAsync(username);
        if (user is null) return NotFound();
        
        ReadUser readUser = ReadUser.FromUser(user);
        
        return Ok(readUser);
    }
}

