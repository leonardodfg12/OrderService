using System.Diagnostics.CodeAnalysis;

namespace OrderService.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}