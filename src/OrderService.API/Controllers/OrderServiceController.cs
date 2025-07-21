using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Handlers;
using OrderService.Domain.Entities;

namespace OrderService.API.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class OrderServiceController(OrderServiceHandler handler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        await handler.Handle(command);
        return Ok(new { message = "Pedido criado com sucesso", pedido = command });
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> GetAll() =>
        await handler.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(string id)
    {
        var order = await handler.GetByIdAsync(id);
        if (order is null) return NotFound();
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await handler.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Cancel(string id)
    {
        await handler.CancelAsync(id);
        return NoContent();
    }
}