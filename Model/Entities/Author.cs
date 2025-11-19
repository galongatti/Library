namespace Library.Model.Entities;

public class Author : BaseEntity
{
    public Author(string name)
    {
        Name = name;
    }
    
    public Author(string name, int id)
    {
        Name = name;
        Id = id;
    }
    public string Name { get; private set; }

    public void Update(string name)
    {
        Name = name;
    }
}