using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateBook
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int CategoryId { get; set; }
    public int PublishedYear {get; set;}
    public Book ToEntity() => new(Title, ISBN, PublishedYear, CategoryId);
    
    
}