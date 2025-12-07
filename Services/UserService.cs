using Library.Exceptions;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private const string InternalUser = "INTERNALUSER";
    private const string CustomerRole = "CUSTOMER";

    public UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User> CreateInternalUserAsync(CreateUser model)
    {
        User user = model.ToEntity();

        await EnsureRoleExistsAsync(InternalUser);
        
        if(await GetUserByUsernameAsync(model.UserName) is not null)
            throw new UserException("User already exists!");

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new UserException("Error creating internal user: " +
                                    string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        await _userManager.AddToRoleAsync(user, InternalUser);

        return user;
    }

    public async Task<User> CreateCustomerAsync(CreateUser model)
    {
        User user = model.ToEntity();

        await EnsureRoleExistsAsync(CustomerRole);
        
        if(await GetUserByUsernameAsync(model.UserName) is not null)
            throw new UserException("User already exists!");

        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            string errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new UserException("Error creating CUSTOMER user: " + errors);
        }

        await _userManager.AddToRoleAsync(user, CustomerRole);

        return user;
    }

    public Task<List<User>> GetUsersAsync() => _userManager.Users.ToListAsync();

    public Task<User?> GetUserByUsernameAsync(string username) =>
        _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);

    private async Task EnsureRoleExistsAsync(string roleName)
    {
        await _roleManager.RoleExistsAsync(roleName);
    }
}