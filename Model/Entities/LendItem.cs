namespace Library.Model.Entities;

public class LendItem : BaseEntity
{
    public int LendId { get; private set; }
    public int BookCopyId { get; private set; }
    public Lend Lend { get; private set; }
    public BookCopy BookCopy { get; private set; }

    public LendItem(int lendId, int bookCopyId)
    {
        LendId = lendId;
        BookCopyId = bookCopyId;
    }
}

