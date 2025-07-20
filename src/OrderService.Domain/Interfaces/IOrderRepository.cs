using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(string id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(string id);
}