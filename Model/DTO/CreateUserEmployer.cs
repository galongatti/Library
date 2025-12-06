using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateUserEmployer
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public User ToEntity()
    {
        return new User
        {
            UserName = UserName,
            Email = Email,
            Document = Document,
            Name = Name
        };
    }
}

