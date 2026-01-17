using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadLendItem
{
    public int LendId { get; private set; }
    public ReadBookCopy BookCopy { get; private set; }

    public ReadLendItem(int lendId, BookCopy bookCopy)
    {
        LendId = lendId;
        BookCopy = ReadBookCopy.FromBook(bookCopy);
    }
    
    public static ReadLendItem FromLendItem(LendItem lendItem) => new(lendItem.LendId, lendItem.BookCopy);
    public static List<ReadLendItem> FromLendsItens(List<LendItem> lendItems) => lendItems.Select(lendItem => FromLendItem(lendItem)).ToList();
}