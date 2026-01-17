namespace Library.Model.Entities;

public class BookCopy : BaseEntity
{
    public int BookId { get; private set; }
    public string Barcode { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    // Navigation
    public Book Book { get; private set; }

    public BookCopy(int bookId, string barcode)
    {
        BookId = bookId;
        Barcode = barcode;
        IsAvailable = true;
    }

    public void MarkAsLent() => IsAvailable = false;
    public void MarkAsReturned() => IsAvailable = true;

    public override void SetAsDeleted()
    {
        IsAvailable = false;
        base.SetAsDeleted();
    }
}

