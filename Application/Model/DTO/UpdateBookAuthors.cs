using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class UpdateBookAuthors
{
    [Required(ErrorMessage = "AuthorIds is required.")]
    [MinLength(1, ErrorMessage = "At least one author is required.")]
    public List<int> AuthorIds { get; set; }
}