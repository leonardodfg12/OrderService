using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;
using OrderService.Application.Handlers;
using OrderService.Domain.Entities;

namespace OrderService.API.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class OrderServiceController(OrderServiceHandler handler) : ControllerBase
{
    [Authorize(Roles = "Cliente, Admin, Cozinha")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        await handler.Handle(command);
        return Ok(new { message = "Pedido criado com sucesso", pedido = command });
    }

    [Authorize(Roles = "Admin, Cozinha")]
    [HttpGet]
    public async Task<IEnumerable<Order>> GetAll() =>
        await handler.GetAllAsync();

    [Authorize(Roles = "Admin, Cozinha")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(string id)
    {
        var order = await handler.GetByIdAsync(id);
        if (order is null) return NotFound();
        return Ok(order);
    }

    [Authorize(Roles = "Admin, Cozinha")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await handler.DeleteAsync(id);
        return NoContent();
    }
}