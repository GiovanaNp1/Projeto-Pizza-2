using Microsoft.AspNetCore.Mvc;
using Pizzaria_Nat_2.Models;
using Pizzaria_Nat_2.Services;

namespace Pizzaria_Nat_2.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController(ILogger<PizzaController> logger)
    : ControllerBase
{
    private readonly ILogger<PizzaController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() {
        _logger.LogInformation("Buscando todas as pizzas");
        var pizzas = PizzaService.GetAll();
        return Ok(pizzas);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById<Guid>(Guid id){
        var pizza = PizzaService.Get(id);
        if(pizza == null) return NotFound();
        return Ok(pizza);
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(GetById), new {id = pizza.Id}, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();
        
        PizzaService.Update(pizza);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
        {
            return NotFound(new { message = "Pizza não encontrada"});
        }
        PizzaService.Remove(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, Pizza pizza)
    {
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
        {
            return NotFound(new { message = "Pizza não encontrada" });
        }

        foreach (var prop  in existingPizza.GetType().GetProperties())
        {
            var propValue = prop.GetValue(existingPizza);
            if (propValue != null)
            {
                prop.SetValue(existingPizza, propValue);
            }
        }
        PizzaService.Update(existingPizza);
        return NoContent();
    }
    
}