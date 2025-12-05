namespace Library.Model.Entities;

public class Author : BaseEntity
{
    public string Name { get; private set; }
    public List<Book> Books { get; private set; } = [];

    public Author(string name)
    {
        Name = name;
    }
    
    public Author(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public void Update(string name)
    {
        Name = name;
    }
}