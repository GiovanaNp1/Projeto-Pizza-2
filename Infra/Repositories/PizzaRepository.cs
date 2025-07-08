using Microsoft.EntityFrameworkCore;
using Pizzaria_Nat_2.Domain.Models;
using Pizzaria_Nat_2.Repositories;

namespace Pizzaria_Nat_2.Infra.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzariaDbContext _context;
        private IPizzaRepository _pizzaRepositoryImplementation;

        public  PizzaRepository(PizzariaDbContext context)
        {
            _context = context;
        }
    
        //private static readonly List<Pizza> _pizzas = new()
        //{
        //new Pizza { Name = "Margherita", Ingredients = ["molho de tomate", "mussarela", "manjericão"], Price = 30.00, IsVegetarian = true, IsGlutenFree = false },
        // new Pizza { Name = "Calabresa", Ingredients = ["molho de tomate", "mussarela", "calabresa", "cebola"], Price = 35.00, IsVegetarian = false, IsGlutenFree = false },
        //   new Pizza { Name = "Veggie", Ingredients = ["molho de tomate", "Tofu", "Beringela"], Price = 25.00, IsVegetarian = true, IsGlutenFree = true }
        //};

        // public IEnumerable<Pizza> GetAll() => _pizzas;
        public IEnumerable<Pizza> GetAll()
        {
            return _context.Pizzas
                .Include(p => p.Ingredients)
                .ToList();
        }
    
        // public Pizza? GetById(Guid id) => _pizzas.FirstOrDefault(p => p.Id == id);
        public Pizza GetById(Guid id) => _context.Pizzas.Find(id);
    
        // public void Add(Pizza pizza) => _pizzas.Add(pizza);
        public void Add(Pizza pizza)
        {
            foreach (var ingredient in pizza.Ingredients)
            {
                _context.Attach(ingredient);
            }
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
        }
        //
        // public void Update(Pizza pizza)
        // {
        //     var index = _pizzas.FindIndex(p => p.Id == pizza.Id);
        //     if (index != -1)
        //     {
        //         _pizzas[index] = pizza;
        //     }
        // }
        public void Update(Pizza pizza)
        {
            _context.Pizzas.Update(pizza);
            _context.SaveChanges();
        }

        // public void Remove(Guid id)
        // {
        //     var pizza = GetById(id);
        //     if (pizza != null)
        //     {
        //         _pizzas.Remove(pizza);
        //     }
        // }
        public void Remove(Guid pizza)
        {
            Pizza p = _context.Pizzas.Find(pizza);
            _context.Pizzas.Remove(p);
            _context.SaveChanges();
        }
    }
}