using Pizzaria_Nat_2.Models;
using Pizzaria_Nat_2.Repositories;

namespace Pizzaria_Nat_2.Infra.Repositories;

public class PizzaRepository : IPizzaRepository
{
    private static readonly List<Pizza> _pizzas = new()
    {
        new Pizza { Name = "Margherita", Ingredients = ["molho de tomate", "mussarela", "manjericão"], Price = 30.00, IsVegetarian = true, IsGlutenFree = false },
        new Pizza { Name = "Calabresa", Ingredients = ["molho de tomate", "mussarela", "calabresa", "cebola"], Price = 35.00, IsVegetarian = false, IsGlutenFree = false },
        new Pizza { Name = "Veggie", Ingredients = ["molho de tomate", "Tofu", "Beringela"], Price = 25.00, IsVegetarian = true, IsGlutenFree = true }
    };

    public IEnumerable<Pizza> GetAll() => _pizzas;

    public Pizza? GetById(Guid id) => _pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza) => _pizzas.Add(pizza);

    public void Update(Pizza pizza)
    {
        var index = _pizzas.FindIndex(p => p.Id == pizza.Id);
        if (index != -1)
        {
            _pizzas[index] = pizza;
        }
    }

    public void Remove(Guid id)
    {
        var pizza = GetById(id);
        if (pizza != null)
        {
            _pizzas.Remove(pizza);
        }
    }
}