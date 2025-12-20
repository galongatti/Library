namespace Library.Model.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public virtual void SetAsDeleted()
    {
        IsDeleted = true;
    }
}