
using Pizzaria_Nat_2.Models;

namespace Pizzaria_Nat_2.Repositories;

public interface IPizzaRepository
{
    IEnumerable<Pizza> GetAll();
    Pizza? GetById(Guid id);
    void Add(Pizza pizza);
    void Update(Pizza pizza);
    void Remove(Guid id);
}