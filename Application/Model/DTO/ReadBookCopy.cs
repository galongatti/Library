using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadBookCopy
{
    public int BookCopyId { get; private set; }
    public string Barcode { get; private set; }
    public bool IsAvailable { get; private set; }
    
    public ReadBookCopy(int bookCopyId, string barcode, bool isAvailable)
    {
        Barcode = barcode;
        IsAvailable = isAvailable;
        BookCopyId = bookCopyId;
    }

    public static ReadBookCopy FromBookCopy(BookCopy bookCopy) => new(
        bookCopyId: bookCopy.Id,
        barcode: bookCopy.Barcode,
        isAvailable: bookCopy.IsAvailable
    );

    public static List<ReadBookCopy> FromBooksCopies(List<BookCopy> books) =>
        books.Select(book => FromBookCopy(book)).ToList();
}