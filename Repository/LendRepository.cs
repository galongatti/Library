using Library.Context;
using Library.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class LendRepository(AppDbContext dbContext) : ILendRepository
{
    public async Task<List<Lend>> GetLendsAsync()
    {
        return await dbContext.Lends
            .Include(l => l.InternalUser)
            .Include(l => l.Costumer)
            .Include(l => l.Items)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Lend?> GetLendByIdAsync(int id)
    {
        return await dbContext.Lends
            .Include(l => l.InternalUser)
            .Include(l => l.Costumer)
            .Include(l => l.Items)
            .AsNoTracking()
            .SingleOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lend>> GetLendsByCustomerIdAsync(string customerUserId)
    {
        return await dbContext.Lends
            .Where(l => l.CustumerUserId == customerUserId)
            .Include(l => l.InternalUser)
            .Include(l => l.Costumer)
            .Include(l => l.Items)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Lend> CreateLendAsync(Lend lend)
    {
        dbContext.Lends.Add(lend);
        await dbContext.SaveChangesAsync();
        return lend;
    }

    public async Task<bool> UpdateLendAsync(Lend lend)
    {
        dbContext.Lends.Update(lend);
        await dbContext.SaveChangesAsync();
        return true;
    }

    // Items
    public async Task<List<LendItem>> GetItemsByLendIdAsync(int lendId)
    {
        return await dbContext.LendItems.Where(i => i.LendId == lendId).Include(i => i.BookCopy).AsNoTracking().ToListAsync();
    }

    public async Task<LendItem> AddItemAsync(LendItem item)
    {
        dbContext.LendItems.Add(item);
        await dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<bool> RemoveItemAsync(int itemId)
    {
        LendItem? item = await dbContext.LendItems.FindAsync(itemId);
        if (item is null) return false;
        dbContext.LendItems.Remove(item);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
