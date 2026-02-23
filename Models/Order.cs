using System.ComponentModel.DataAnnotations;

namespace ECom_wep_app.Models;

public class Order : IValidatableObject
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be greater than 0.")]
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public List<OrderItem> OrderItems { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (OrderItems == null || OrderItems.Count == 0)
        {
            yield return new ValidationResult(
                "An order must include at least one order item.",
                new[] { nameof(OrderItems) }
            );
        }
    }
}
