using DrinkItUpWebApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DrinkItUpTests
{
    public class DrinkContextInMemory : DbContext
    {
        private DbContextOptionsBuilder<DrinkContextInMemory> optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDatabase");

        

        public DrinkContextInMemory(DbContextOptions<DrinkContextInMemory> options) : base(options)
        {

        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<MainAlcohol> MainAlcohols { get; set; }
        public DbSet<DrinkIngredient> DrinkIngredients { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }
}
