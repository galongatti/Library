using Library.Model.Entities;

namespace Library.Services;

public interface ITokenService
{
    string GenerateToken(User user, IList<string> roles);
}

