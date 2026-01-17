using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadCategory
{
    public string Name { get; }
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public bool IsDeleted { get; }
    
    public ReadCategory(string name, int id, DateTime createdAt, bool isDeleted)
    {
        Name = name;
        Id = id;
        CreatedAt = createdAt;
        IsDeleted = isDeleted;
    }

    public static ReadCategory FromCategory(Category catego) => new(catego.Name, catego.Id, catego.CreatedAt, catego.IsDeleted);
    public static List<ReadCategory> FromCategories(List<Category> categories) => categories.Select(category => FromCategory(category)).ToList();
}