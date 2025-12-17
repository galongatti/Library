namespace Library.Model.Entities;

public class Lend : BaseEntity
{
    public string InternalUserId { get; private set; }
    public string CostumerUserId { get; private set; }
    public User InternalUser { get; private set; } 
    public User Costumer { get; private set; }
    public DateTime LendDate { get; private set; }
    public DateTime? ExpectedReturnDate { get; private set; }
    public LendStatus Status { get; private set; } = LendStatus.Pending;

    public Lend(string internalUserId, string costumerUserId,DateTime lendDate)
    {
        InternalUserId = internalUserId;
        CostumerUserId = costumerUserId;
        LendDate = lendDate;
    }
    
    public void SetUser(User user)  => InternalUser = user;
    
    public void ApproveLend(DateTime expectedReturnDate)
    {
        ExpectedReturnDate = expectedReturnDate;
        Status = LendStatus.Lent;
    }
    
    public void ReturnLend() => Status = LendStatus.Returned;
  
    public void MarkAsOverdue() => Status = LendStatus.Overdue;
    
    public void CancelLend() => Status = LendStatus.Cancelled;
    
    // Calculates the expected return date based on the lend date and the quantity of days
    public DateTime CalculateLendDuration(int quantityOfDays)
    {
        return LendDate.AddDays(quantityOfDays);
    }
    
    
    
 
}