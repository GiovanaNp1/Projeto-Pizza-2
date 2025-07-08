using Microsoft.EntityFrameworkCore;
using Pizzaria_Nat_2.Domain.Models;

namespace Pizzaria_Nat_2.Infra.Repositories
{
    public class PizzariaDbContext: DbContext
    {
        public PizzariaDbContext(DbContextOptions<PizzariaDbContext> options) : base(options) { }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Pizzas);
        }
        public DbSet<Pizza> Pizzas => Set<Pizza>();
    }
}