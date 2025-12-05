namespace Library.Model.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public ICollection<Book> Books { get; private set; }
    
    public Category(string name)
    {
        Name = name;
    }
    
    public Category(string name, int id)
    {
        Name = name;
        Id = id;
    }
    
    public void Update(string name)
    {
        Name = name;
    }
}