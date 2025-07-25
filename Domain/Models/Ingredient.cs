namespace Pizzaria_Nat_2.Domain.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
