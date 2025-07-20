namespace OrderService.Application.Commands;

public class CreateOrderItemCommand
{
    public string MenuItemId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}