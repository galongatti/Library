using Library.Model.Entities;

namespace Library.Repository;

public interface ILendRepository
{
    Task<List<Lend>> GetLendsAsync();
    Task<Lend?> GetLendByIdAsync(int id);
    Task<List<Lend>> GetLendsByCustomerIdAsync(string customerUserId);
    Task<Lend> CreateLendAsync(Lend lend);
    Task<bool> UpdateLendAsync(Lend lend);
}

