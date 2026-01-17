using Library.Model.DTO;
using Library.Model.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Library.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AuthController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ITokenService tokenService,
        IUserService userService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        User? user = await _userService.GetUserByUsernameAsync(model.Username);
        if (user is null) return Unauthorized();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return Unauthorized();

        IList<string> roles = await _userService.GetRolesAsync(user);
        string token = _tokenService.GenerateToken(user, roles);

        return Ok(token);
    }
}
