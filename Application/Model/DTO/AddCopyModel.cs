namespace Library.Model.DTO;
using System.ComponentModel.DataAnnotations;

public class AddCopyModel
{
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Barcode must be between 1 and 100 characters.")]
    [Required(ErrorMessage = "Barcode is required.")]
    public string Barcode { get; set; }
}

