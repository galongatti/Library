using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class AddLendItemModel
{
    [Required(ErrorMessage = "BookCopyId is required.")]
    public int BookCopyId { get; set; }
}

