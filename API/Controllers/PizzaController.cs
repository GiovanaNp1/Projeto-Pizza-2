using Microsoft.AspNetCore.Mvc;
using Pizzaria_Nat_2.Domain.Models;
using Pizzaria_Nat_2.Services;
namespace Pizzaria_Nat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;

    public PizzaController(PizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var pizzas = _pizzaService.GetAll();
        return Ok(pizzas);
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var pizza = _pizzaService.GetById(id);
        if (pizza == null) return NotFound();
        return Ok(pizza);
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        _pizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = _pizzaService.GetById(id);
        if (existingPizza == null)
            return NotFound();

        _pizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var existingPizza = _pizzaService.GetById(id);
        if (existingPizza == null)
            return NotFound();

        _pizzaService.Remove(id);
        return NoContent();
    }
}