using Library.Exceptions;
using Library.Model;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class LendService(ILendRepository repository, IBookService bookService) : ILendService
{
    public async Task<List<Lend>> GetLendsAsync()
    {
        return await repository.GetLendsAsync();
    }

    public async Task<Lend> GetLendByIdAsync(int id)
    {
        Lend? lend = await repository.GetLendByIdAsync(id);
        return lend ?? throw new LendException("Lend not found");
    }

    public async Task<Lend> CreateLendAsync(CreateLend model)
    {
        Lend lend = model.ToEntity();
        return await repository.CreateLendAsync(lend);
    }

    public async Task<bool> ApproveLendAsync(int lendId, DateTime expectedReturnDate)
    {
        Lend? lend = await repository.GetLendByIdAsync(lendId);
        if (lend is null) throw new LendException("Lend not found");
        if (lend.Status != LendStatus.Pending) throw new LendException("Only pending lends can be approved");

        lend.ApproveLend(expectedReturnDate);
        return await repository.UpdateLendAsync(lend);
    }

    public async Task<bool> ReturnLendAsync(int lendId)
    {
        Lend? lend = await repository.GetLendByIdAsync(lendId);
        if (lend is null) throw new LendException("Lend not found");
        if (lend.Status != LendStatus.Lent) throw new LendException("Only lent lends can be returned");

        lend.ReturnLend();
        return await repository.UpdateLendAsync(lend);
    }

    public async Task<bool> CancelLendAsync(int lendId)
    {
        Lend? lend = await repository.GetLendByIdAsync(lendId);
        if (lend is null) throw new LendException("Lend not found");
        if (lend.Status != LendStatus.Pending) throw new LendException("Only pending lends can be cancelled");

        lend.CancelLend();
        return await repository.UpdateLendAsync(lend);
    }

    public async Task<List<LendItem>> GetItemsByLendIdAsync(int lendId)
    {
        return await repository.GetItemsByLendIdAsync(lendId);
    }

    public async Task<LendItem> AddItemAsync(int lendId, AddLendItemModel bookCopyId)
    {
        Lend? lend = await repository.GetLendByIdAsync(lendId);
        if (lend is null) throw new LendException("Lend not found");
        if (lend.Status != LendStatus.Pending) throw new LendException("Can only add items to a pending lend");

        bool marked = await bookService.MarkCopyAsLentAsync(bookCopyId.BookCopyId);
        if (!marked) throw new LendException("Book copy not available");

        LendItem item = new(lendId, bookCopyId.BookCopyId);
        return await repository.AddItemAsync(item);
    }

    public async Task<bool> RemoveItemAsync(int lendId, int itemId)
    {
        Lend? lend = await repository.GetLendByIdAsync(lendId);
        
        if(lend is null) throw new LendException("Lend not found");
        
        LendItem? item = lend.Items.SingleOrDefault(i => i.Id == itemId);

        if (item is null) throw new LendException("Item not found");

        bool removed = await repository.RemoveItemAsync(itemId);
        if (!removed) return false;

        await bookService.MarkCopyAsReturnedAsync(item.BookCopyId);
        return true;
    }
}
