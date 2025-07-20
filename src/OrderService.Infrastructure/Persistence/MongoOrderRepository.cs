using MongoDB.Driver;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Infrastructure.Persistence;

public class MongoOrderRepository(IMongoDatabase database) : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection = database.GetCollection<Order>("Orders");

    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<Order?> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(Order order) =>
        await _collection.InsertOneAsync(order);

    public async Task UpdateAsync(Order order) =>
        await _collection.ReplaceOneAsync(x => x.Id == order.Id, order);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}