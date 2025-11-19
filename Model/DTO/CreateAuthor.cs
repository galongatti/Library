using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateAuthor
{
    public string Name { get; set; }

    public Author ToEntity() => new(Name);

}