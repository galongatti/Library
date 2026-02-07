using Library.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class CreateLend
{
    [Required(ErrorMessage = "InternalUserId is required.")]
    public string InternalUserId { get; set; }
    
    [Required(ErrorMessage = "CostumerUserId is required.")]
    public string CostumerUserId { get; set; }
    
    public Lend ToEntity() => new(InternalUserId, CostumerUserId, DateTime.UtcNow);
}

