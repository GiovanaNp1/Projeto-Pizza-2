using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Pizzaria_Nat_2.Domain.Models;
using Pizzaria_Nat_2.Repositories;
using System.Text.Json;

namespace Pizzaria_Nat_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public PedidosController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        string cacheKey = $"pedido:{id}";
        var pedidoJson = await _cache.GetStringAsync(cacheKey);

        if (pedidoJson == null)
        {
            return NotFound();
        }

        return Ok(new { origem = "cache", dados = pedidoJson });
    }

    [HttpPost]
    public async Task<CreatedAtActionResult> Add([FromBody] Pedido pedido)
    {
        var json = JsonSerializer.Serialize(pedido);

        string cacheKey = $"pedido:{pedido.Id}";
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        await _cache.SetStringAsync(cacheKey, json, options);
        return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _cache.Remove($"pedido:{id}");
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] Pedido pedido)
    {
        var json = JsonSerializer.Serialize(pedido);
        _cache.SetString($"pedido:{id}", json, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });

        return NoContent();
    }
}
