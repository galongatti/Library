using Library.Model.DTO;
using Library.Model.Entities;

namespace Library.Services;

public interface ILendService
{
    Task<List<Lend>> GetLendsAsync();
    Task<Lend> GetLendByIdAsync(int id);
    Task<Lend> CreateLendAsync(CreateLend model);
    Task<bool> ApproveLendAsync(int lendId, DateTime expectedReturnDate);
    Task<bool> ReturnLendAsync(int lendId);
    Task<bool> CancelLendAsync(int lendId);
}
