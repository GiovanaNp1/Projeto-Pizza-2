namespace Pizzaria_Nat_2.Domain.Models;

public class Pizza
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new();
    public double Price { get; set; }
    public bool IsVegetarian { get; set; }
    public bool IsGlutenFree { get; set; }
}