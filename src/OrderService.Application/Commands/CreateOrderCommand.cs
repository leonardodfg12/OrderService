namespace OrderService.Application.Commands;

public class CreateOrderCommand
{
    public string CustomerName { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public List<CreateOrderItemCommand> Items { get; set; } = new();
}