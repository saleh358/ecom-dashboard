using System.ComponentModel.DataAnnotations;

namespace ECom_wep_app.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0.")]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }

    public Order? Order { get; set; }
}
