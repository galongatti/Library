using Library.Model.Entities;

namespace Library.Model.DTO;

public class CreateLend
{
    public string InternalUserId { get; set; }
    public string CostumerUserId { get; set; }
    public Lend ToEntity() => new(InternalUserId, CostumerUserId, DateTime.UtcNow);
}

