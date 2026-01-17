using System.ComponentModel.DataAnnotations;
using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateUser
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Document { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage =
            "Password must be at least 8 characters long and contain an uppercase letter, a lowercase letter, a digit, and a special character.")]
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