namespace OrderService.Application.Contracts;

public class OrderStatusChangedEvent
{
    public string Id { get; set; } = default!;
    public string NewStatus { get; set; } = default!;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}