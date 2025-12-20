using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadBookCopy
{
    public int BookCopyId { get; private set; }
    public int BookId { get; private set; }
    public string Barcode { get; private set; }
    public bool IsAvailable { get; private set; }
    
    public ReadBookCopy(int bookCopyId ,int bookId, string barcode, bool isAvailable)
    {
        BookId = bookId;
        Barcode = barcode;
        IsAvailable = isAvailable;
        BookCopyId = bookCopyId;
    }

    public static ReadBookCopy FromBook(BookCopy bookCopy) => new(
        bookCopyId: bookCopy.Id,
        bookId: bookCopy.BookId,
        barcode: bookCopy.Barcode,
        isAvailable: bookCopy.IsAvailable
    );

    public static List<ReadBookCopy> FromBooksCopies(List<BookCopy> books) =>
        books.Select(book => FromBook(book)).ToList();
}