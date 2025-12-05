namespace Library.Model.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string ISBN { get; private set; }
    public List<Author> Authors { get; private set; } = [];
    public int PublishedYear { get; private set; } 
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }
    
    public Book(string title, string ISBN, int publishedYear, int categoryId)
    {
        Title = title;
        this.ISBN = ISBN;
        PublishedYear = publishedYear;
        CategoryId = categoryId;
    }

    public void Update(string title, string isbn, int publishedYear, int categoryId)
    {
        Title = title;
        ISBN = isbn;
        PublishedYear = publishedYear;
        CategoryId = categoryId;
    }
    
    public void SetAuthors(List<Author> authors)
    {
        Authors = authors;
    }
}