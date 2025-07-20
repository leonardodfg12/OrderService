using System.Diagnostics.CodeAnalysis;
using MassTransit;
using MenuService.Infrastructure.Configurations;
using MongoDB.Driver;
using OrderService.Application.Handlers;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Persistence;

namespace OrderService.API;

[ExcludeFromCodeCoverage]
public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddMassTransit(
            cfg =>
            {
                cfg.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration["RABBITMQ_HOST"], h =>
                    {
                        h.Username(builder.Configuration["RABBITMQ_USERNAME"]!);
                        h.Password(builder.Configuration["RABBITMQ_PASSWORD"]!);
                    });
                });
            });

        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = builder.Configuration["MONGO_CONNECTION_STRING"];
            return new MongoClient(connectionString);
        });

        builder.Services.AddScoped(sp =>
        {
            var databaseName = builder.Configuration["MONGO_DATABASE_NAME"];
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(databaseName);
        });

        builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();
        builder.Services.AddScoped<OrderServiceHandler>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}