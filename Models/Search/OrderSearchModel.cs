namespace ECom_wep_app.Models.Search;

public class OrderSearchModel
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
    public DateTime? OrderDate { get; set; }
}
