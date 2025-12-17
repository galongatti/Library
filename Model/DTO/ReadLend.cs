using Library.Model.Entities;

namespace Library.Model.DTO;

public class ReadLend
{
    public int Id { get; set; }
    public string InternalUserId { get; set; }
    public string CostumerUserId { get; set; }
    public DateTime LendDate { get; set; }
    public DateTime? ExpectedReturnDate { get; set; }
    public LendStatus Status { get; set; }

    public static ReadLend FromLend(Lend lend) => new()
    {
        Id = lend.Id,
        InternalUserId = lend.InternalUserId,
        CostumerUserId = lend.CostumerUserId,
        LendDate = lend.LendDate,
        ExpectedReturnDate = lend.ExpectedReturnDate,
        Status = lend.Status
    };

    public static List<ReadLend> FromLends(List<Lend> lends) => lends.Select(FromLend).ToList();
}

