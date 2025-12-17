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
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Lend?> GetLendByIdAsync(int id)
    {
        return await dbContext.Lends
            .Include(l => l.InternalUser)
            .Include(l => l.Costumer)
            .AsNoTracking()
            .SingleOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lend>> GetLendsByCustomerIdAsync(string customerUserId)
    {
        return await dbContext.Lends
            .Where(l => l.CostumerUserId == customerUserId)
            .Include(l => l.InternalUser)
            .Include(l => l.Costumer)
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
}

