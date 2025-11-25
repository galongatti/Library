using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateCategory
{
    public string Name { get; set; }
    
    public Category ToEntity() => new(Name);
}