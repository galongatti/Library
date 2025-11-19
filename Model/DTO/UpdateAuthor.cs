using Library.Model.Entities;

namespace Library.Model.DTO;

public class UpdateAuthor   
{
    public string Name { get; set; }
    
    public Author ToEntity() => new(Name);
}