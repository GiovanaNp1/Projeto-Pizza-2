using Pizzaria_Nat_2.Domain.Models;
using Pizzaria_Nat_2.Repositories;

namespace Pizzaria_Nat_2.Services;

public class PizzaService
{
    private readonly IPizzaRepository _repository;

    public PizzaService(IPizzaRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Pizza> GetAll() => _repository.GetAll();

    public Pizza? GetById(Guid id) => _repository.GetById(id);

    public void Add(Pizza pizza) => _repository.Add(pizza);

    public void Update(Pizza pizza) => _repository.Update(pizza);

    public void Remove(Guid id) => _repository.Remove(id);

    public static Pizza Get(Guid id)
    {
        throw new NotImplementedException();
    }
}