using Library.Model.DTO;
using Library.Model.Entities;


namespace Library.Services;

public interface IUserService
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<List<User>> GetUsersAsync();
    Task<User> CreateCustomerAsync(CreateUser model);
    Task<User> CreateInternalUserAsync(CreateUser model);
}

