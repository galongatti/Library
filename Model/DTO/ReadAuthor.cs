using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadAuthor
{
    public string Name { get; }
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public bool IsDeleted { get; }
    
    public ReadAuthor(string name, int id, DateTime createdAt, bool isDeleted)
    {
        Name = name;
        Id = id;
        CreatedAt = createdAt;
        IsDeleted = isDeleted;
    }

    public static ReadAuthor FromAuthor(Author author) => new(author.Name, author.Id, author.CreatedAt, author.IsDeleted);

}