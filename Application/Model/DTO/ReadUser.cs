using System.ComponentModel.DataAnnotations;
using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadUser
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Document { get; set; }

    public string Name { get; set; }
    
    public string Id { get; set; }
    
    public ReadUser(string userName, string email, string document, string name, string id)
    {
        UserName = userName;
        Email = email;
        Document = document;
        Name = name;
        Id = id;
    }
    
    public static ReadUser FromUser(User user) => new(user.UserName, user.Email, user.Document, user.Name, user.Id);
    public static List<ReadUser> FromUsers(List<User> users) => users.Select(user => FromUser(user)).ToList();


    
}