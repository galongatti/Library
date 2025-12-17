using Library.Exceptions;
using Library.Model;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;

namespace Library.Services;

public class LendService(ILendRepository repository) : ILendService
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
}
