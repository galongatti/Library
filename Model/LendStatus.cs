namespace Library.Model;

public enum LendStatus
{
    // The lend is requested but not yet approved
    Pending = 0,
    // The lend is currently active
    Lent = 1,
    // The lend has been returned
    Returned = 2,
    // The lend is overdue
    Overdue = 3,
    // The lend has been cancelled
    Cancelled = 4
}