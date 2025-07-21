using MassTransit;
using OrderService.Application.Commands;
using OrderService.Application.Contracts;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Handlers;

public class OrderServiceHandler(IOrderRepository repository, ISendEndpointProvider sendEndpointProvider)
{
    private const string OrderStatusChangedEvent = "order-status-changed";
    private const string OrderCreatedEvent = "order-created";

    public async Task Handle(CreateOrderCommand command)
    {
        var order = new Order
        {
            CustomerName = command.CustomerName,
            DeliveryMethod = command.DeliveryMethod,
            Items = command.Items.Select(i => new OrderItem
            {
                MenuItemId = i.MenuItemId,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList()
        };

        await repository.AddAsync(order);

        var orderCreated = new OrderCreatedEvent
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Total = order.Total,
            CreatedAt = order.CreatedAt,
            DeliveryMethod = order.DeliveryMethod
        };

        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{OrderCreatedEvent}"));
        await endpoint.Send(orderCreated);
    }

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await repository.GetAllAsync();

    public async Task<Order?> GetByIdAsync(string id) =>
        await repository.GetByIdAsync(id);

    public async Task DeleteAsync(string id) =>
        await repository.DeleteAsync(id);
}