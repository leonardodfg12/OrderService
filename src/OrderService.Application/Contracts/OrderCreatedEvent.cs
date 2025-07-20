namespace OrderService.Application.Contracts;

public class OrderCreatedEvent
{
    public string Id { get; set; } = default!;
    public string CustomerName { get; set; } = default!;
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DeliveryMethod { get; set; } = default!;
}