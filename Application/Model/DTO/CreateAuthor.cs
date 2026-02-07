using Library.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class CreateAuthor
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 255 characters.")]
    public string Name { get; set; }

    public Author ToEntity() => new(Name);

}