using System.ComponentModel.DataAnnotations;

namespace Library.Model.DTO;

public class ApproveLend
{
    [Required(ErrorMessage = "ExpectedReturnDate is required.")]
    [DataType(DataType.Date)]
    [FutureDate(ErrorMessage = "ExpectedReturnDate must be a future date.")]
    public DateTime ExpectedReturnDate { get; set; }
}

