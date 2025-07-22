using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderService.Domain.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public string? Justification { get; set; }
    public string CustomerName { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!; // ex: "Retirada", "Entrega"
    public List<OrderItem> Items { get; set; } = new();
    public string Status { get; private set; } = "Pendente";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal Total => Items.Sum(i => i.Total);

    public void Accept() => Status = "Aceito";
    public void Reject() => Status = "Rejeitado";
    public void Cancel() => Status = "Cancelado";
}