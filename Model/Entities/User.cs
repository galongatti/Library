using Microsoft.AspNetCore.Identity;

namespace Library.Model.Entities;

public class User : IdentityUser
{
    public string Document { get; set; }
    public string Name { get; set; }
}