using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class DrinkContext : DbContext
    {
        public DrinkContext(DbContextOptions<DrinkContext> options) : base(options)
        {

        }

        public DbSet<Drink> Drinks { get;set; }
        public DbSet<Difficulty> Difficulties { get;set; }
        public DbSet<MainAlcohol> MainAlcohols { get;set; }
        public DbSet<DrinkIngredient> DrinkIngredients  { get;set; }
        public DbSet<Unit> Units { get;set; }
        public DbSet<Ingredient> Ingredients { get;set; }
        
    }
}
