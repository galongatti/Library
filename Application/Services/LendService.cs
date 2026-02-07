using Library.Context;
using Library.Exceptions;
using Library.Model;
using Library.Model.DTO;
using Library.Model.Entities;
using Library.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Services;

public class LendService(
    ILendRepository repository,
    IBookService bookService,
    AppDbContext dbContext,
    ILogger<LendService> logger) : ILendService
{
    public async Task<List<Lend>> GetLendsAsync()
    {
        logger.LogDebug("Retrieving all lends");
        var lends = await repository.GetLendsAsync();
        logger.LogInformation("Retrieved {Count} lends", lends.Count);
        return lends;
    }

    public async Task<Lend> GetLendByIdAsync(int id)
    {
        using (logger.BeginScope(new Dictionary<string, object> { ["LendId"] = id }))
        {
            logger.LogDebug("Retrieving lend by ID: {LendId}", id);

            Lend? lend = await repository.GetLendByIdAsync(id);

            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", id);
                throw new LendException("Lend not found");
            }

            logger.LogDebug("Lend found: {LendId} with status {Status}", id, lend.Status);
            return lend;
        }
    }

    public async Task<Lend> CreateLendAsync(CreateLend model)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "CreateLend",
                   ["InternalUserId"] = model.InternalUserId,
                   ["CustomerUserId"] = model.CostumerUserId
               }))
        {
            logger.LogInformation("Creating new lend for customer {CustomerUserId}", model.CostumerUserId);

            Lend lend = model.ToEntity();
            var createdLend = await repository.CreateLendAsync(lend);

            logger.LogInformation("Lend created successfully with ID: {LendId}", createdLend.Id);

            return createdLend;
        }
    }

    public async Task<bool> ApproveLendAsync(int lendId, DateTime expectedReturnDate)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "ApproveLend",
                   ["LendId"] = lendId,
                   ["ExpectedReturnDate"] = expectedReturnDate
               }))
        {
            logger.LogInformation("Attempting to approve lend {LendId}", lendId);

            Lend? lend = await repository.GetLendByIdAsync(lendId);

            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", lendId);
                throw new LendException("Lend not found");
            }

            if (lend.Status != LendStatus.Pending)
            {
                logger.LogWarning("Cannot approve non-pending lend. Current status: {Status}", lend.Status);
                throw new LendException("Only pending lends can be approved");
            }

            lend.ApproveLend(expectedReturnDate);
            var result = await repository.UpdateLendAsync(lend);

            if (result)
            {
                logger.LogInformation("Lend {LendId} approved successfully", lendId);
            }

            return result;
        }
    }

    public async Task<bool> ReturnLendAsync(int lendId)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "ReturnLend",
                   ["LendId"] = lendId
               }))
        {
            logger.LogInformation("Attempting to return lend {LendId}", lendId);

            Lend? lend = await repository.GetLendByIdAsync(lendId);

            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", lendId);
                throw new LendException("Lend not found");
            }

            if (lend.Status != LendStatus.Lent)
            {
                logger.LogWarning("Cannot return non-lent lend. Current status: {Status}", lend.Status);
                throw new LendException("Only lent lends can be returned");
            }

            lend.ReturnLend();
            var result = await repository.UpdateLendAsync(lend);

            if (result)
            {
                logger.LogInformation("Lend {LendId} returned successfully", lendId);
            }

            return result;
        }
    }

    public async Task<bool> CancelLendAsync(int lendId)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "CancelLend",
                   ["LendId"] = lendId
               }))
        {
            logger.LogInformation("Attempting to cancel lend {LendId}", lendId);

            Lend? lend = await repository.GetLendByIdAsync(lendId);

            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", lendId);
                throw new LendException("Lend not found");
            }

            if (lend.Status != LendStatus.Pending)
            {
                logger.LogWarning("Cannot cancel non-pending lend. Current status: {Status}", lend.Status);
                throw new LendException("Only pending lends can be cancelled");
            }

            lend.CancelLend();
            var result = await repository.UpdateLendAsync(lend);

            if (result)
            {
                logger.LogInformation("Lend {LendId} cancelled successfully", lendId);
            }

            return result;
        }
    }

    public async Task<List<LendItem>> GetItemsByLendIdAsync(int lendId)
    {
        return await repository.GetItemsByLendIdAsync(lendId);
    }

    public async Task<LendItem> AddItemAsync(int lendId, AddLendItemModel bookCopyId)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "AddLendItem",
                   ["LendId"] = lendId,
                   ["BookCopyId"] = bookCopyId.BookCopyId
               }))
        {
            logger.LogInformation("Starting transaction to add item to lend: {LendId}, BookCopy: {BookCopyId}",
                lendId, bookCopyId.BookCopyId);

            Lend? lend = await repository.GetLendByIdAsync(lendId);
            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", lendId);
                throw new LendException("Lend not found");
            }

            if (lend.Status != LendStatus.Pending)
            {
                logger.LogWarning("Cannot add item to non-pending lend. Status: {Status}", lend.Status);
                throw new LendException("Can only add items to a pending lend");
            }

            logger.LogDebug("Lend found and validated. Status: {Status}", lend.Status);

            logger.LogDebug("Marking book copy as lent: {BookCopyId}", bookCopyId.BookCopyId);
            bool marked = await bookService.MarkCopyAsLentAsync(bookCopyId.BookCopyId);

            if (!marked)
            {
                logger.LogWarning("Book copy not available: {BookCopyId}", bookCopyId.BookCopyId);
                throw new LendException("Book copy not available");
            }

            logger.LogDebug("Book copy marked as lent successfully");

            LendItem item = new(lendId, bookCopyId.BookCopyId);
            logger.LogDebug("Adding item to lend");
            LendItem addedItem = await repository.AddItemAsync(item);

            logger.LogInformation(
                "Transaction committed successfully. Item {ItemId} added to lend {LendId}",
                addedItem.Id,
                lendId
            );

            return addedItem;
        }
    }

    public async Task<bool> RemoveItemAsync(int lendId, int itemId)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   ["Operation"] = "RemoveLendItem",
                   ["LendId"] = lendId,
                   ["ItemId"] = itemId
               }))
        {
            logger.LogInformation("Starting transaction to remove item {ItemId} from lend {LendId}", itemId, lendId);


            Lend? lend = await repository.GetLendByIdAsync(lendId);

            if (lend is null)
            {
                logger.LogWarning("Lend not found: {LendId}", lendId);
                throw new LendException("Lend not found");
            }

            if (lend.Status != LendStatus.Pending)
            {
                logger.LogWarning("Cannot remove item from non-pending lend. Status: {Status}", lend.Status);
                throw new LendException("Can only remove items to a pending lend");
            }

            LendItem? item = lend.Items.SingleOrDefault(i => i.Id == itemId);

            if (item is null)
            {
                logger.LogWarning("Item {ItemId} not found in lend {LendId}", itemId, lendId);
                throw new LendException("Item not found");
            }

            logger.LogDebug("Found item {ItemId} with BookCopyId {BookCopyId}", itemId, item.BookCopyId);

            logger.LogDebug("Removing item from database");
            bool removed = await repository.RemoveItemAsync(itemId);

            if (!removed)
            {
                logger.LogWarning("Failed to remove item {ItemId} from database", itemId);
                throw new LendException("Failed to remove item");
            }

            logger.LogDebug("Marking book copy {BookCopyId} as returned", item.BookCopyId);
            await bookService.MarkCopyAsReturnedAsync(item.BookCopyId);

            logger.LogInformation(
                "Transaction committed successfully. Item {ItemId} removed from lend {LendId}",
                itemId,
                lendId
            );

            return true;
        }
    }
}