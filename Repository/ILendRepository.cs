using Library.Model.Entities;

namespace Library.Repository;

public interface ILendRepository
{
    Task<List<Lend>> GetLendsAsync();
    Task<Lend?> GetLendByIdAsync(int id);
    Task<List<Lend>> GetLendsByCustomerIdAsync(string customerUserId);
    Task<Lend> CreateLendAsync(Lend lend);
    Task<bool> UpdateLendAsync(Lend lend);

    // Items
    Task<List<LendItem>> GetItemsByLendIdAsync(int lendId);
    Task<LendItem> AddItemAsync(LendItem item);
    Task<bool> RemoveItemAsync(int itemId);
}
