namespace Library.Model.DTO;

public class UpdateBook
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int CategoryId { get; set; }
    public int PublishedYear {get; set;}
}