using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadBook
{
    public string Title { get; }
    public int Id { get; }
    public DateTime CreatedAt { get; }
    public bool IsDeleted { get; }
    public ReadCategory Category { get; }
    public string ISBN { get; }
    public List<ReadAuthor> Author { get; }

    public ReadBook(string title, int id, DateTime createdAt, bool isDeleted, Category category, string ISBN, List<Author> authors = null)
    {
        Title = title;
        this.ISBN = ISBN;
        Id = id;
        CreatedAt = createdAt;
        IsDeleted = isDeleted;
        Category = ReadCategory.FromCategory(category);
        Author = ReadAuthor.FromAuthors(authors);
    }

    public static ReadBook FromBook(Book book) => new(
        title: book.Title,
        id: book.Id,
        createdAt: book.CreatedAt,
        isDeleted: book.IsDeleted,
        category: book.Category,
        ISBN: book.ISBN,
        authors: book.Authors
    );

    public static List<ReadBook> FromBooks(List<Book> books) =>
        books.Select(book => FromBook(book)).ToList();
}