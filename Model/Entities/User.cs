using Microsoft.AspNetCore.Identity;

namespace Library.Model.Entities;

public class User : IdentityUser
{
    public required string Document { get; set; }
    public required string Name { get; set; }
}