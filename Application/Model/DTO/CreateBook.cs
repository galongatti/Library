using Library.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class CreateBook
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 255 characters.")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "ISBN is required.")]
    [StringLength(17, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 17 characters.")]
    [RegularExpression(@"^(?:ISBN(?:-1[03])?:?\s?)?(?=[0-9X]{10}$|(?=(?:[0-9]+[-\s]){3})[-\s0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[-\s]){4})[-\s0-9]{17}$)(?:97[89][-\s]?)?[0-9]{1,5}[-\s]?[0-9]+[-\s]?[0-9]+[-\s]?[0-9X]$",
        ErrorMessage = "Invalid ISBN format. Must be a valid ISBN-10 or ISBN-13.")]
    public string ISBN { get; set; }
    
    [Required(ErrorMessage = "CategoryId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "PublishedYear is required.")]
    [Range(1000, 2026, ErrorMessage = "PublishedYear must be between 1000 and current year.")]
    public int PublishedYear {get; set;}
    
    public Book ToEntity() => new(Title, ISBN, PublishedYear, CategoryId);
}