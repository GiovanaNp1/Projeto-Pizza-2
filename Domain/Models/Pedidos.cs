namespace Pizzaria_Nat_2.Domain.Models;

public class Pedido
{
    public Guid Id { get; set; }
    public string Produto { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}