using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public DbSet<User> Users{ get;set; }
        public DbSet<Role> Roles { get;set; }
        public DbSet<Like> Likes { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new Role[]
                {
                    new Role {RoleId = 1, Name = "Administrator"},
                    new Role {RoleId = 2, Name = "Użytkownik Premium"},
                    new Role {RoleId = 3, Name = "Użytkownik"}
                    
                });

        }

    }
}
