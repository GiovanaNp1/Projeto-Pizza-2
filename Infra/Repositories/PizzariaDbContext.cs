using Microsoft.EntityFrameworkCore;
using Pizzaria_Nat_2.Domain.Models;

namespace Pizzaria_Nat_2.Infra.Repositories;

public class PizzariaDbContext: DbContext
{
    public PizzariaDbContext(DbContextOptions<PizzariaDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>()
            .HasMany(p => p.Ingredients)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "Pizza_Ingredient",
                j => j.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId"),
                j => j.HasOne<Pizza>().WithMany().HasForeignKey("PizzaId")
                );
    }
    public DbSet<Pizza> Pizzas => Set<Pizza>();
}