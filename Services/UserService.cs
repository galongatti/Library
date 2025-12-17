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

        bool checkRoleExistsAsync = await CheckRoleExistsAsync(InternalUser);
        
        if(!checkRoleExistsAsync)
            throw new UserException("InternalUser role doesn't exist: " + InternalUser);
        
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

        bool checkRoleExistsAsync = await CheckRoleExistsAsync(CustomerRole);
        
        if(!checkRoleExistsAsync)
            throw new UserException("Customer role doesn't exist: " + CustomerRole);
        
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

    public Task<IList<string>> GetRolesAsync(User user) => _userManager.GetRolesAsync(user);
    private async Task<bool> CheckRoleExistsAsync(string roleName) => await _roleManager.RoleExistsAsync(roleName);
   
}